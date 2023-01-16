using QuickVR;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickStageSaveToFile : QuickStageBase
{
	public int id;
	public QuickStageSaveToMatrix matrixRef;
	public Animator animator;
	//private string _performanceFile1, _performanceFile2, _performanceFile3;
	//private StreamWriter fout1, fout2, fout3;


	public override void Init()
	{
		base.Init();

		for (int i = 0; i < 3; i++)
		{
			var _performanceFile = Application.dataPath + @"/../../../OutputData/" + SceneManager.GetActiveScene().name + "/subject" + id + "/performance" + i +".csv";
			var fout = new StreamWriter(_performanceFile);
			getBoneHeader(animator.transform, fout);
			fout.WriteLine();

			List<float[]> m = new List<float[]>();
			if (i == 1)
				m = matrixRef.GetM1();
			else if(i == 2)
				m = matrixRef.GetM2();
			else if (i == 3)
				m = matrixRef.GetM3();

			WriteToFile(m, fout);
			fout.Close();
		}

		//_performanceFile1 = Application.dataPath + @"/../../../OutputData/" + SceneManager.GetActiveScene().name + "/subject" + id + "/performance1.csv";
		//_performanceFile2 = Application.dataPath + @"/../../../OutputData/" + SceneManager.GetActiveScene().name + "/subject" + id + "/performance2.csv";
		//_performanceFile3 = Application.dataPath + @"/../../../OutputData/" + SceneManager.GetActiveScene().name + "/subject" + id + "/performance3.csv";

		//fout1 = new StreamWriter(_performanceFile1);
		//fout2 = new StreamWriter(_performanceFile2);
		//fout3 = new StreamWriter(_performanceFile3);

		//getBoneHeader(animator.transform, fout1);
		//getBoneHeader(animator.transform, fout2);
		//getBoneHeader(animator.transform, fout3);

		//fout1.WriteLine();
		//fout2.WriteLine();
		//fout3.WriteLine();

		//var m1 = matrixRef.GetM1();
		//var m2 = matrixRef.GetM2();
		//var m3 = matrixRef.GetM3();

		//WriteToFile(m1, fout1);
		//WriteToFile(m2, fout2);
		//WriteToFile(m3, fout3);

		//fout1.Close();
		//fout2.Close();
		//fout3.Close();

	}

	private static void WriteToFile(List<float[]> m1, StreamWriter fout)
	{
		for (int i = 0; i < m1.Count; i++)
		{
			for (int j = 0; j < 24*6; j++)
			{
				fout.Write(m1[i][j].ToString("F4") + ", ");
			}

			fout.WriteLine();
		}
	}

	private void getBoneHeader(Transform p, StreamWriter f)
	{
		if (p.name.Contains("__") || p.name.Contains("_IK") || p.name.Contains("Mesh") || p.name.Contains("Body") || p.name.Contains("Hair")) return;

		f.Write(p.name + "-posX, ");
		f.Write(p.name + "-posY, ");
		f.Write(p.name + "-posZ, ");

		f.Write(p.name + "-rotX, ");
		f.Write(p.name + "-rotY, ");
		f.Write(p.name + "-rotZ, ");

		if (p.name.Contains("hand")) return; // Do not write fingers header

		for (int i = 0; i < p.childCount; i++)
		{
			var child = p.GetChild(i);
			if (!child.name.Contains("__") && !child.name.Contains("_IK") && !child.name.Contains("Mesh") && !p.name.Contains("Body") && !p.name.Contains("Hair"))
				getBoneHeader(child, f);
		}

	}
}
