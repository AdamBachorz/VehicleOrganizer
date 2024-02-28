namespace VehicleOrganizer.MobileApp;

[Activity(Label = "OtherActivity")]
public class OtherActivity : Activity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        // Create your application here
        SetContentView(Resource.Layout.other_activity);
    }
}