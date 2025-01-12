using System.Collections.Generic;

namespace _10_War
{
    public class Platoon
    {
        private readonly List<Solder> _solders;

        public Platoon(List<Solder> solders)
        {
            _solders = solders;
        }

        public bool IsAlive => _solders.Count > 0;
        public IReadOnlyList<Solder> GetSolders() => _solders;

        public void Attack(IReadOnlyList<Solder> solders)
        {
            for (int i = 0; i < _solders.Count; i++)
            {
                _solders[i].Attack(solders);
            }
        }

        public void RemoveDeadSolders()
        {
            int solderCount = _solders.Count;

            for (int i = solderCount - 1; i >= 0; i--)
            {
                Solder currentSolder = _solders[i];

                if (currentSolder.IsALive == false)
                    _solders.Remove(currentSolder);
            }
        }
    }
}
