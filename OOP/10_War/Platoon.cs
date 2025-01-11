using System.Collections.Generic;

namespace _10_War
{
    public class Platoon
    {
        private readonly List<Warrior> _solders;

        public Platoon(List<Warrior> solders)
        {
            _solders = solders;
        }

        public bool IsAlive => _solders.Count > 0;

        public IReadOnlyList<Warrior> GetSolders() => _solders;

        public void Attack(IReadOnlyList<Warrior> solders)
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
                Warrior currentSolder = _solders[i];

                if (currentSolder.IsALive == false)
                    _solders.Remove(currentSolder);
            }
        }
    }
}
