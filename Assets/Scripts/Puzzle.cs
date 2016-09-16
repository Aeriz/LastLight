using UnityEngine;
using System.Collections;
using VolumetricLines.Utils;
namespace VolumetricLines
{
    public class Puzzle : MonoBehaviour
    {
        public int numMirrors;
        public GameObject[] mirrors;
        public LineRenderer[] lines;
        public GameObject[] lights;
        public GameObject[] volumetricLines;
        public Transform[][] linePositions;
        //public VolumetricLineBehavior[] linesScripts;
        //public MeshRenderer[] mesh;

        Vector3 incidenceAngle;
        Vector3 reflectionAngle;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            /*
            mesh = new MeshRenderer[volumetricLines.Length];
            linesScripts = new VolumetricLineBehavior[volumetricLines.Length];
            for(int i = 0; i < volumetricLines.Length; i++)
            {
                linesScripts[i] = volumetricLines[i].GetComponent<VolumetricLineBehavior>();
                mesh[i] = volumetricLines[i].GetComponent<MeshRenderer>();
                linesScripts[i].enabled = false;
                mesh[i].enabled = false;
            }
            */
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i].enabled = false;
            }
            for (int i = 0; i < lights.Length; i++)
            {
                RayTest(lights[i], i);
            }
        }

        bool RayTest(GameObject raySource, int i)
        {
            RaycastHit hit;
            if (Physics.Raycast(raySource.transform.position, raySource.transform.forward, out hit, Mathf.Infinity))
            {

                incidenceAngle = hit.point - raySource.transform.position;
                reflectionAngle = Vector3.Reflect(incidenceAngle, hit.normal);
                Debug.DrawRay(raySource.transform.position, hit.point - raySource.transform.position, Color.white);
                lines[i].enabled = true;
                lines[i].SetPosition(0, raySource.transform.position);
                lines[i].SetPosition(1, hit.point);
                //linesScripts[i].enabled = true;
                //mesh[i].enabled = true;
                //linesScripts[i].m_startPos = raySource.transform.position;
                //linesScripts[i].m_endPos = hit.point;
                //linesScripts[i].SetStartAndEndPoints(raySource.transform.position, hit.point);

                if (hit.transform.tag == "Mirror")
                {
                    i++;
                    MirrorRayTest(hit, i);
                }
                return true;

            }
            return false;
        }

        bool MirrorRayTest(RaycastHit raySource, int i)
        {
            RaycastHit hit;
            if (Physics.Raycast(raySource.point, reflectionAngle, out hit, Mathf.Infinity))
            {
                incidenceAngle = hit.point - raySource.point;
                Debug.DrawRay(raySource.point, incidenceAngle, Color.white);
                lines[i].enabled = true;
                lines[i].SetPosition(0, raySource.point);
                lines[i].SetPosition(1, hit.point);
                //linesScripts[i].enabled = true;
                //mesh[i].enabled = true;
                //linesScripts[i].m_startPos = raySource.transform.position;
                //linesScripts[i].m_endPos = hit.point;
                //linesScripts[i].SetStartAndEndPoints(raySource.transform.position, hit.point);
                reflectionAngle = Vector3.Reflect(incidenceAngle, hit.normal);
                if (hit.transform.tag == "Mirror")
                {
                    i++;
                    MirrorRayTest(hit, i++);
                }
                return true;
            }
            return false;
        }
    }
}






