using System;
using Engine;
using Game;
using GameEntitySystem;
using TemplatesDatabase;

namespace Yun
{
	// Token: 0x02000003 RID: 3
	public class ToolClass
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002398 File Offset: 0x00000598
		public void YunUp(int type)
		{
			DatabaseObject databaseObject = GameManager.Project.GameDatabase.Database.FindDatabaseObject("YunBeiBao", GameManager.Project.GameDatabase.EntityTemplateType, true);
			ValuesDictionary valuesDictionary = new ValuesDictionary();
			valuesDictionary.PopulateFromDatabaseObject(databaseObject);
			switch (type)
			{
			case 0:
				valuesDictionary.GetValue<ValuesDictionary>("Yun").SetValue<Point3>("Coordinates", new Point3(0, 0, 1));
				break;
			case 1:
				valuesDictionary.GetValue<ValuesDictionary>("Yun").SetValue<Point3>("Coordinates", new Point3(0, 1, 1));
				break;
			case 2:
				valuesDictionary.GetValue<ValuesDictionary>("Yun").SetValue<Point3>("Coordinates", new Point3(0, 2, 1));
				break;
			case 3:
				valuesDictionary.GetValue<ValuesDictionary>("Yun").SetValue<Point3>("Coordinates", new Point3(0, 3, 1));
				break;
			}
			Entity entity = GameManager.Project.CreateEntity(valuesDictionary);
			GameManager.Project.AddEntity(entity);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002498 File Offset: 0x00000698
		public void CreateUp()
		{
			DatabaseObject databaseObject = GameManager.Project.GameDatabase.Database.FindDatabaseObject("CreateTable", GameManager.Project.GameDatabase.EntityTemplateType, true);
			ValuesDictionary valuesDictionary = new ValuesDictionary();
			valuesDictionary.PopulateFromDatabaseObject(databaseObject);
			valuesDictionary.GetValue<ValuesDictionary>("BlockEntity").SetValue<Point3>("Coordinates", new Point3(1, 1, 1));
			Entity entity = GameManager.Project.CreateEntity(valuesDictionary);
			GameManager.Project.AddEntity(entity);
		}
	}
}
