using Gefangenendilemma.Basis;

namespace GefangenenDilemma
{
    static class VerbrechensModus
    {
        public static IVerbrechensModus Set(Verbrechen verbrechen)
        {
            switch (verbrechen)
            {
                case Verbrechen.Leicht:
                    return new LeichterVerstoß();
                case Verbrechen.Mittel:
                    return new MittlererVerstoß();
                case Verbrechen.Schwer:
                    return new SchwererVerstoß();
                default:
                    return null;
            }
        }
    }
}
