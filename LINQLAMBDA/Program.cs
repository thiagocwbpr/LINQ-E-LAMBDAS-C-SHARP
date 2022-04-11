using System;
using System.Linq;
using LINQLAMBDA.Entities;
using System.Collections.Generic;

namespace LINQLAMBDA {
    internal class Program {

        static void Print<T>(string message, IEnumerable<T> collection) {
            Console.WriteLine(message);
            foreach  (T obj in collection) {
                Console.WriteLine(obj);
            }
            Console.WriteLine();
        }
        static void Main(string[] args) {

            Category c1 = new Category() { ID = 1, Name = "Tools", Tier = 2 };
            Category c2 = new Category() { ID = 2, Name = "Computers", Tier = 1 };
            Category c3 = new Category() { ID = 3, Name = "Electronics", Tier = 1 };


            List<Product> Products = new List<Product>() {
            new Product(){ ID = 1, Name = "Computer ", Price = 1100.0, Category =  c2},
            new Product(){ ID = 2,  Name = "Hammer ", Price = 90.0, Category = c1 },
            new Product(){ ID = 3,  Name = "TV ", Price = 1700.0, Category = c3},
            new Product(){ ID = 4,  Name = "Notebook ", Price = 1300.0, Category = c2},
            new Product(){ ID = 5,  Name = "SAW ", Price = 80.0, Category = c1},
            new Product(){ ID = 6,  Name = "Tablet ", Price = 700.0, Category = c2},
            new Product(){ ID = 7,  Name = "Camera ", Price = 700.0, Category = c3},
            new Product(){ ID = 8,  Name = "Printer ", Price = 350.0, Category = c3},
            new Product(){ ID = 9,  Name = "MacBook ", Price = 1800.0, Category = c2},
            new Product(){ ID = 10, Name = "Sound Bar ", Price = 700.0, Category = c3},
            new Product(){ ID = 11, Name = "Level ", Price = 70.0, Category = c1}
            };
            // Na linha abaixo, é praticamente um IF dentro de uma lambda.
            var r1 = Products.Where(p => p.Category.Tier == 1 && p.Price > 900.0);
            // Na linha abaixo, estamos buscando o retorno da categoria, porém queremos somente o nome
            var r2 = Products.Where(p => p.Category.Name == "Tools").Select(p => p.Name);
            /* Na linha abaixo, queremos saber apenas os nome de produtos que começam com a letra C.
            porém, quero ter o retorno de nome, preço e categoria */
            var r3 = Products.Where(p => p.Name[0] == 'C').Select(p => new { p.Name, p.Price, CategoryName = p.Category.Name}) ;
            /* Na linha abaixo, queremos somente o Tier 1 dos produtos, que seja ordenado o preço dos produtos,
             e depois retornar o nome do produto. */
            var r4 = Products.Where(p => p.Category.Tier == 1).OrderBy(p => p.Price).ThenBy(p => p.Name);
            /* Na linha abaixo, estamos pegando os dados da linha de cima, e descartando as 2 primeiras linhas
             e pegando as 4 proximas linhas. */
            var r5 = r4.Skip(2).Take(4);
            Print("NAMES OF PRODUCTS FROM TOOLS ", r5);
            // Aqui queremos saber somente os dados da primiera linha da nossa lista.
            var r6 = Products.FirstOrDefault(); 
            Console.WriteLine("Default Test " + r6);
            /* Aqui estamos filtrando os produtos, que tem o valor acima de 3.000, é lógico que não vamos ter produto
             com esse valor na nossa lista, porém é pra demonstrar o FirstOrDefault, para que retorne nulo. */
            var r7 = Products.Where(p => p.Price > 3000.0).FirstOrDefault();
            Console.WriteLine("Default Fisrt Test " + r7);
            // Na linha abaixo, temos o SingleOrDefault, que vai retornar somente a linha do ID 3.
            var r8 = Products.Where(p => p.ID == 3).SingleOrDefault();
            Console.WriteLine(" r8 Single or Default test1: "+ r8);
            // Na linha abaixo, mostra na tela um campo nulo. Isso evita uma excessão, até porque não temos a ID 30.
            var r9 = Products.Where(p => p.ID == 30).SingleOrDefault();
            Console.WriteLine("Ninguem Single or Default test1: " + r9);
            // Na linha abaixo, vai retornar o valor máximo do produto que temos na nossa lista.
            var r10 = Products.Max(p => p.Price);
            Console.WriteLine("Max Price " + r10);
            // Na linha abaixo, vai retornar o valor mínimo do produto que temos na nossa lista.
            var r11 = Products.Min(p => p.Price);
            Console.WriteLine("Min Price: " + r11);
            // Na linha abaixo, queremos saber os produtos da categoria ID 1, qual será a soma desses produtos.
            var r12 = Products.Where(p => p.Category.ID == 1).Sum(p => p.Price);
            Console.WriteLine("Sum Category 1 : " + r12);
            // Na linha abaixo, queremos saber os produtos da categoria ID 1, porém qual é a media de preço desses produtos.
            var r13 = Products.Where(p => p.Category.ID == 1).Average(p => p.Price);
            Console.WriteLine("Average : " + r13);
            /* Na linha abaixo, estamos buscando os precos da categoria ID 1, porém se retornar zero, o  DefaultIfEmpty
             vai tratar com 0.0 ou nulo, e no final vamos criar uma média disso, portanto o DefaultIfEmpaty é 
            um tratamento de excessão praticamente. */
            var r14 = Products.Where(p => p.Category.ID == 1).Select(p => p.Price).DefaultIfEmpty(0.0).Average();
            Console.WriteLine("Average r14 : " + r14);
            /* Na linha abaixo, estamos criando uma agregação, É o tal do Map Reduce de big data, que no caso
             do C# é o SELECT (Que seria o Map ) E AGGREGATE (Que seria o Reduce). No select nos vamos informar de 
            qual campo queremos extrair os dados,no caso é do Price. E na agregação criamos uma função anonima
            para coletar os dados, e note que dentro do aggregate temos 0.0. Isso significa um tratamento de excessão,
            se caso o valor for 0 ou puxarmos uma categoria de ID que não existe. */
            var r15 = Products.Where(p => p.Category.ID == 1).Select(p => p.Price).Aggregate(0.0,(x, y) => x + y);
            Console.WriteLine("Average r15 : " + r15);
            Console.WriteLine("");
            /* Na linha abaixo, criamos um agrupamento de dados, que no caso está buscando dados da categoria.
             note que o foreach é necessário, Note também que nosso foreach é criado de forma especial. */
            var r16 = Products.GroupBy(p => p.Category);
            //Na linha abaixo, esse foreach permite grupos de cara categoria pelo nome.
            foreach (IGrouping<Category, Product> group in r16) {
                Console.WriteLine("Category" + group.Key.Name + ":");
                // Nesse segundo foreach, ele vai puxar as informações dos produtos de cada categoria.
                foreach (Product p in group) {
                    Console.WriteLine(p);
                }
                Console.WriteLine("");
            }
        }
    }

}

