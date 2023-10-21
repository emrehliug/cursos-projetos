using Newtonsoft.Json;
using System;
using System.Net;

namespace Bot.MegaSena
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Informe o numero do concurso:");
            string numeroConcurso = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(numeroConcurso))
            {
                numeroConcurso = "2103";
            }

            string url = "http://loterias.caixa.gov.br/wps/portal/loterias/landing/megasena/!ut/p/a1/04_Sj9CPykssy0xPLMnMz0vMAfGjzOLNDH0MPAzcDbwMPI0sDBxNXAOMwrzCjA0sjIEKIoEKnN0dPUzMfQwMDEwsjAw8XZw8XMwtfQ0MPM2I02-AAzgaENIfrh-FqsQ9wNnUwNHfxcnSwBgIDUyhCvA5EawAjxsKckMjDDI9FQE-F4ca/dl5/d5/L2dBISEvZ0FBIS9nQSEh/pw/Z7_HGK818G0KO6H80AU71KG7J0072/res/id=buscaResultado/c=cacheLevelPage/=/?timestampAjax=1600883624693&concurso=" + numeroConcurso;
            string json;
            
            using(WebClient wc = new WebClient())
            {
                wc.Headers["Cookie"] = "security=true";
                json = wc.DownloadString(url);
            }

            Result Resultado = JsonConvert.DeserializeObject<Result>(json);

            Console.WriteLine("Concurso selecionado: " + numeroConcurso);
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Resultado: " + Resultado.resultado);
            Console.WriteLine("Resultado Odernado: " + Resultado.resultadoOrdenado);
            if (Resultado.sorteioAcumulado.Equals(true))
            {
                Console.WriteLine("Concurso Acumulado!");
                Console.WriteLine("Valor Acumulado: " + Resultado.valor_acumulado);
            }
            else
            {
                Console.WriteLine("Concurso não Acumulado!");
            }

            Console.ReadKey();
        }
    }
}
