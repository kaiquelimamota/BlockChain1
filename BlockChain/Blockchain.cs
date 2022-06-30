// See https://aka.ms/new-console-template for more information

using BlockChain.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;


namespace BlockChain
{
    public class Blockchain
    {
        public List<Bloco> chain = new List<Bloco> ();
        private string prefixo ;
        private int difficulty ;
        private helpers helper = new helpers();

        public Blockchain(string pref = "0", int diff = 4)
        {
            this.prefixo = pref;
            this.difficulty = diff;
            var block = this.criarBlocoGenesis();
            var minerado = mineirarBloco(block);
            this.chain.Add(minerado.bloco);
            
        }

        public Bloco pegarUltimoBloco()
        {
            return this.chain.Last();
        }

        public List<Bloco> pegarBlockchain()
        {
            return this.chain;
        }

        public string getPreviousBlockHash()
        {
            return this.chain.Last().header.hashBloco;
        }
        public Bloco criarBlocoGenesis()
        {

            var bloco = new Bloco ();

            var payload = new Payload()
            {
                sequencia = 0,
                timestamp = DateTime.Now.ToString("yyyyMMddHHmmssffff"),
                dados = "Bloco Inicial",
                hashAnterior = ""
            };
            string json = JsonConvert.SerializeObject(payload);
            var header = new Header()
            {
                nonce = 0,
                hashBloco = helper.createHash(json)
            };
            bloco.header = header;
            bloco.payload = payload;

            return bloco;
        }

        public Bloco criarBloco()
        {
            
            var bloco = new Bloco();

            var payload = new Payload()
            {
                sequencia = this.chain.Last().payload.sequencia + 1,
                timestamp = DateTime.Now.ToString("yyyyMMddHHmmssffff"),
                dados = $"novaTransação",
                hashAnterior = getPreviousBlockHash(),
            };
            
            bloco.payload = payload;

            return bloco;
        }

        public blocoMinerado mineirarBloco(Bloco bloco)
        {
            var nonce = 0;
            var inicio = DateTime.Now;

            
            while (true) 
            {
                string json = JsonConvert.SerializeObject(bloco.payload);
                string hashBloco = helper.createHash(json);

                string hashPow = helper.createHash(hashBloco + nonce);
                if (helper.hashValidado(hashPow, difficulty, prefixo))
                {
                    var final = DateTime.Now;
                    var tempoMineraçao = (final - inicio) / 1000;
                    var hashReduzido = hashPow.Substring(0, 12);
                    Console.WriteLine($"bloco {bloco.payload.sequencia} mineirado em {tempoMineraçao}");
                    Console.WriteLine($"hash: {hashReduzido}... {nonce} tentativas");
                    bloco.header = new Header();
                    bloco.header.nonce = nonce;
                    bloco.header.hashBloco = hashPow;

                    return new blocoMinerado()
                    {
                        bloco = bloco,
                        hash = bloco.header.hashBloco,
                        hashCurto = bloco.header.hashBloco.Substring(0, 12),
                        tempoDeMineração = tempoMineraçao
                    };
                }
                nonce++;    
            } 

        }

        public List<Bloco> enviarBloco(Bloco bloco)
        {
            if (verificarBloco(bloco))
            {
                chain.Add(bloco);
                Console.WriteLine($"bloco {bloco.payload.sequencia} foi adicionado a blockchain {JsonConvert.SerializeObject(chain)}");
            }
            return chain;
        }

        private bool verificarBloco(Bloco bloco)
        {
            if (bloco.payload.hashAnterior != getPreviousBlockHash())
            {
                Console.WriteLine($"Bloco {bloco.payload.sequencia} invalido, o hash anterior é {getPreviousBlockHash()} e nao {bloco.payload.hashAnterior}");
                return false;
            }

            if (!helper.hashValidado( helper.createHash(helper.createHash(JsonConvert.SerializeObject(bloco.payload) ) + bloco.header.nonce), difficulty , prefixo))
            {
                Console.WriteLine($"Bloco {bloco.payload.sequencia} Não Tem um hash valido");
                return false;
            }

            return true;
        }

        //private string ComputeSha256Hash(string json)
        //{
        //    using (SHA256 sha256Hash = SHA256.Create())
        //    {
        //        // ComputeHash - returns byte array  
        //        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(json));

        //        // Convert byte array to a string   
        //        StringBuilder builder = new StringBuilder();
        //        for (int i = 0; i < bytes.Length; i++)
        //        {
        //            builder.Append(bytes[i].ToString("x2"));
        //        }
        //        return builder.ToString();
        //    }
        //}
    }

   

    

    
}