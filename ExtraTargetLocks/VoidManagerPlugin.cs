using VoidManager.MPModChecks;

namespace ExtraTargetLocks
{
    public class VoidManagerPlugin : VoidManager.VoidPlugin
    {
        public override MultiplayerType MPType => MultiplayerType.Client;

        public override string Author => "Dragon";

        public override string Description => "Increases max target locks. ClientSide";
    }
}
