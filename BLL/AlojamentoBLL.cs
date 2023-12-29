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
        /// Manipular alojamentos
        /// </summary>
        /// <returns></returns>
        public List<Alojamento> GetAllAlojamentos()
        {
            return alojamentoDAL.GetAllAlojamentos();
        }
        /// <summary>
        /// Adicionar alojamentos
        /// </summary>
        /// <param name="alojamento"></param>
        public void AdicionaAlojamentos(Alojamento alojamento)
        {
            var alojamentos = GetAllAlojamentos();
            alojamentos.Add(alojamento);
            alojamentoDAL.GravarAlojamentos(alojamentos);
        }

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

        public void EditaAlojamento(List<Alojamento> alojamentos, string nome, int camas, double espaco, double preco, int pos)
        {
            alojamentos[pos].nome = nome;
            alojamentos[pos].camas = camas;
            alojamentos[pos].espaco = espaco;
            alojamentos[pos].preco = preco;

            alojamentoDAL.GravarAlojamentos(alojamentos);
        }

        public void EliminarAlojamento(List<Alojamento> alojamentos, int codigo)
        {
            // Procura o alojamento com base no código
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