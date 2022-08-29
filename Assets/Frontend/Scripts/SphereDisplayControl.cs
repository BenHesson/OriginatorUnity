using OriginatorKids.Communication.Http;
using OriginatorKids.Communication.Http.Exceptions;
using OriginatorKids.DataModels;
using OriginatorKids.Parser;
using OriginatorKids.Parser.Exceptions;
using System;
using UnityEngine;

public class SphereDisplayControl : MonoBehaviour
{
    private const string SPHERE_URL = "http://www.originatorkids.com/hiring/hex_cells.txt";

    FrontEndDataIngester dataIngestor;
    
    void Start()
    {
        CreateSphere();
    }

    public void CreateSphere()
    {
        //First let's check out dependencies
        CheckDependencies();

        //Second let's try and get the data we need from the server
        try
        {
            StartCoroutine(new HttpCommunicator(SPHERE_URL).Send(OnServerDataReceived));
        }
        catch (HttpRequestException e)
        {
            Debug.LogError($"HttpRequestException trying to fetch sphere data from server: {e.Message} \nStacktrace: {e.StackTrace}");
            return;
        }
        catch(Exception e)
        {
            Debug.LogError($"Exception trying to fetch sphere data from server: {e.Message} \nStacktrace: {e.StackTrace}");
            return;
        }

        
    }

    void OnServerDataReceived(string sphereData)
    {
        //Third, with the data from the server let's try and parse it and create our object of type ISphere
        ISphere sphere = null;
        try
        {
            sphere = (ISphere)new Parser<ISphere>().Parse(sphereData);
        }
        catch (ParseException e)
        {
            Debug.LogError($"ParseException while parsing sphere data: {e.Message} \nStacktrace: {e.StackTrace}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Exception while parsing sphere data: {e.Message} \nStacktrace: {e.StackTrace}");
        }

        //Last let's pass the data to the front end to show it onscreen
        foreach (var poly in sphere.Polygons)
        {
            dataIngestor.DisplayHexCell(poly.CanonicalCenter, poly.VertexPoints);
        }
    }

    void CheckDependencies()
    {
        if (FrontEndAPI.shared == null)
            throw new Exception($"FrontEndAPI DataIngester is null. You probably need to either " +
                $"add the component to the scene in the editor or instantiate one at runtime.");

        dataIngestor = FrontEndAPI.shared.GetDataIngester();
    }
}
