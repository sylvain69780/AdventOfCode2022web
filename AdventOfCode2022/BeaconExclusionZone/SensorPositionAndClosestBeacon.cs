namespace Domain.BeaconExclusionZone
{
    public struct SensorPositionAndClosestBeacon
    {
        public (int x, int y) Sensor;
        public (int x, int y) Beacon;
        public int ManhattanDistance;
    }
}
