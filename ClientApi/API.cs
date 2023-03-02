using System.Collections.Generic;
using System.Linq;

namespace ElectionResultCalculator
{
    public class ElectionResult
    {
        private List<Candidat> _candidats = new List<Candidat>();
        private List<int> _candidatVotes = new List<int>();

        public class Candidat
        {
            public string Name;
            public int Votes;
            public int Id;

            public Candidat(int id, string name, int votes)
            {
                Id = id;
                Name = name;
                Votes = votes;
            }
        }

        public List<int> CreerListCandidats(int nbCandidats)
        {
            for (int i = 1; i <= nbCandidats; i++)
            {
                var candidat = new Candidat(id: i, name: $"Candidat {i}", votes: 0);
                _candidats.Add(candidat);
                _candidatVotes.Add(0);
            }

            return _candidatVotes;
        }

        public void AjouterVotePourCandidat(int candidatId, int percentage)
        {
            var candidat = _candidats.SingleOrDefault(c => c.Id == candidatId);
            if (candidat != null)
            {
                candidat.Votes += percentage;
                _candidatVotes = _candidats.Select(c => c.Votes).ToList();
                _candidats = _candidats.OrderByDescending(c => c.Votes).ToList();
            }
            else
            {
                candidat.Votes += 0;
            }
        }

        public int CalculerElectionResultat(List<int> nbVotesOfCandidats)
        {
            for (int i = 0; i < nbVotesOfCandidats.Count; i++)
            {
                _candidats[i].Votes = nbVotesOfCandidats[i];
            }

            return _candidats[0].Id;
        }

        public List<int> CandidatsInSecondRound()
        {
            List<int> candidats = new List<int>();
            List<int> sortedVotes = _candidatVotes.OrderByDescending(v => v).ToList();
            int maxVotes = sortedVotes[0];
            int secondMaxVotes = sortedVotes[1];

            for (int i = 0; i < _candidatVotes.Count; i++)
            {
                if (_candidatVotes[i] == maxVotes)
                {
                    candidats.Add(i + 1);
                }
                else if (_candidatVotes[i] == secondMaxVotes)
                {
                    candidats.Add(i + 1);
                }
            }

            return candidats;
        }


        public static bool SecondRoundRequis(List<int> votes)
        {
            int totalVotes = votes.Sum();
            int maxVotes = votes.Max();

            if (maxVotes > totalVotes / 2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static List<int> GetCandidatsForSecondRound(List<int> votes)
        {
            List<int> candidats = new List<int>();
            int maxVotes = votes.Max();

            for (int i = 0; i < votes.Count; i++)
            {
                if (votes[i] == maxVotes)
                {
                    candidats.Add(i + 1);
                }
            }

            return candidats;
        }

        public bool ShouldHoldSecondRound()
        {
            return ElectionResult.SecondRoundRequis(_candidatVotes);
        }
    }
}
