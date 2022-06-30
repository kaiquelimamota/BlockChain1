// See https://aka.ms/new-console-template for more information

using BlockChain;
using BlockChain.Models;
using Newtonsoft.Json;

var dificuldade = 4;
var numBlocos = 10;
var chain = new List<Bloco>();

var blockchain = new Blockchain(diff: dificuldade);

for (int i = 1; i < numBlocos; i++)
{
    var bloco = blockchain.criarBloco();
    var mineInfo = blockchain.mineirarBloco(bloco);
    chain = blockchain.enviarBloco(mineInfo.bloco);
}

Console.WriteLine("----------- Blockchain Gerada ------------------");
Console.WriteLine(JsonConvert.SerializeObject(chain));

