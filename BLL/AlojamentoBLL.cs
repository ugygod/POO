using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    public class AlojamentoBLL
    {

        private AlojamentoDAL alojamentoDAL;

        public AlojamentoBLL()
        {
            alojamentoDAL = new AlojamentoDAL();
        }

        #region Alojamentos
        /// <summary>
        /// Obtém a lista completa de alojamentos
        /// </summary>
        /// <returns> Uma lista de objetos Alojamento que representa todos os alojamentos armazenados </returns>
        public List<Alojamento> GetAllAlojamentos()
        {
            return alojamentoDAL.GetAllAlojamentos();
        }

        /// <summary>
        /// Adiciona um novo alojamento à lista de alojamentos
        /// </summary>
        /// <param name="alojamento"> O objeto Alojamento a ser adicionado </param>
        public void AdicionaAlojamentos(Alojamento alojamento)
        {
            var alojamentos = GetAllAlojamentos();
            alojamentos.Add(alojamento);
            alojamentoDAL.GravarAlojamentos(alojamentos);
        }

        /// <summary>
        /// Obtém a posição na lista de um alojamento com um código específico
        /// </summary>
        /// <param name="alojamentos"> A lista de alojamentos a ser pesquisada </param>
        /// <param name="codigo"> O código do alojamento a ser procurado </param>
        /// <returns> A posição na lista do alojamento encontrado ou -1 se nenhum correspondente for encontrado </returns>
        public int GetAlojamento(List<Alojamento> alojamentos, int codigo)
        {
            int pos = -1;
            for (int i = 0; i < alojamentos.Count; i++)
            {
                if (alojamentos[i].codigo == codigo)
                {
                    pos = i;
                }
            }
            return pos;
        }

        /// <summary>
        /// Edita as informações de um alojamento na lista
        /// </summary>
        /// <param name="alojamentos"> A lista de alojamentos a ser modificada </param>
        /// <param name="nome"> O novo nome do alojamento </param>
        /// <param name="camas"> O novo número de camas do alojamento </param>
        /// <param name="espaco"> O novo espaço em metros quadrados do alojamento </param>
        /// <param name="preco"> O novo preço por pessoa do alojamento </param>
        /// <param name="pos"> A posição na lista do alojamento a ser editado </param>
        public void EditaAlojamento(List<Alojamento> alojamentos, string nome, int camas, double espaco, double preco, int pos)
        {
            alojamentos[pos].nome = nome;
            alojamentos[pos].camas = camas;
            alojamentos[pos].espaco = espaco;
            alojamentos[pos].preco = preco;

            alojamentoDAL.GravarAlojamentos(alojamentos);
        }

        /// <summary>
        /// Elimina um alojamento da lista com base no código
        /// </summary>
        /// <param name="alojamentos"> A lista de alojamentos a ser modificada </param>
        /// <param name="codigo"> O código do alojamento a ser removido </param>
        public void EliminarAlojamento(List<Alojamento> alojamentos, int codigo)
        {
            var alojamentoParaEliminar = alojamentos.FirstOrDefault(r => r.codigo == codigo);

            if (alojamentoParaEliminar != null)
            {
                alojamentos.Remove(alojamentoParaEliminar);
                alojamentoDAL.GravarAlojamentos(alojamentos);
                Console.WriteLine("\nAlojamento removido com sucesso!");
            }
            else
            {
                Console.WriteLine($"Alojamento com código {codigo} não encontrada.");
            }
        }

        #endregion


    }
}