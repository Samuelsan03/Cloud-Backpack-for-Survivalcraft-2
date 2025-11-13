using System;
using System.Xml.Linq;
using Engine;
using Game;

namespace Yun
{
	// Token: 0x02000005 RID: 5
	public class YunWidget : CanvasWidget
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002914 File Offset: 0x00000B14
		public YunWidget(ComponentPlayer player, ComponentChest chest, int index)
		{
			this.YunChest = chest;
			this.entities = player.Project.FindSubsystem<SubsystemBlockEntities>(true);
			this.player = player;
			this.index = index;
			XElement xelement = ContentManager.Get<XElement>("Widgets/CloudChestWidget", null);
			base.LoadContents(this, xelement);
			this.inventortGrid = this.Children.Find<GridPanelWidget>("InventoryGrid", true);
			this.chestGrid = this.Children.Find<GridPanelWidget>("ChestGrid", true);
			this.ArrowLeftButton = this.Children.Find<ButtonWidget>("ArrowLeftButton", true);
			this.ArrowRightButton = this.Children.Find<ButtonWidget>("ArrowRightButton", true);
			int num = 0;
			for (int i = 0; i < this.chestGrid.RowsCount; i++)
			{
				for (int j = 0; j < this.chestGrid.ColumnsCount; j++)
				{
					InventorySlotWidget inventorySlotWidget = new InventorySlotWidget();
					inventorySlotWidget.Size = new Vector2(55f, 55f);
					inventorySlotWidget.AssignInventorySlot(chest, num++);
					this.chestGrid.Children.Add(inventorySlotWidget);
					this.chestGrid.SetWidgetCell(inventorySlotWidget, new Point2(j, i));
				}
			}
			num = 10;
			for (int k = 0; k < this.inventortGrid.RowsCount; k++)
			{
				for (int l = 0; l < this.inventortGrid.ColumnsCount; l++)
				{
					InventorySlotWidget inventorySlotWidget2 = new InventorySlotWidget();
					inventorySlotWidget2.AssignInventorySlot(player.ComponentMiner.Inventory, num++);
					this.inventortGrid.Children.Add(inventorySlotWidget2);
					this.inventortGrid.SetWidgetCell(inventorySlotWidget2, new Point2(l, k));
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002AF4 File Offset: 0x00000CF4
		private void SwitchWidget(int index)
		{
			bool flag = this.entities.GetBlockEntity(0, index, 1) == null;
			bool flag2 = flag;
			if (flag2)
			{
				this.tools.YunUp(index);
			}
			else
			{
				this.player.ComponentGui.ModalPanelWidget = new YunWidget(this.player, this.entities.GetBlockEntity(0, index, 1).Entity.FindComponent<ComponentChest>(true), index);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002B64 File Offset: 0x00000D64
		public override void Update()
		{
			this.ArrowLeftButton.IsEnabled = (this.index > 0);
			this.ArrowRightButton.IsEnabled = (this.index < 3);
			bool isClicked = this.ArrowRightButton.IsClicked;
			bool flag = isClicked;
			if (flag)
			{
				this.SwitchWidget(this.index + 1);
			}
			bool isClicked2 = this.ArrowLeftButton.IsClicked;
			bool flag2 = isClicked2;
			if (flag2)
			{
				this.SwitchWidget(this.index - 1);
			}
		}

		// Token: 0x04000011 RID: 17
		private SubsystemBlockEntities entities;

		// Token: 0x04000012 RID: 18
		private ComponentPlayer player;

		// Token: 0x04000013 RID: 19
		private ComponentChest YunChest;

		// Token: 0x04000014 RID: 20
		private GridPanelWidget inventortGrid;

		// Token: 0x04000015 RID: 21
		private GridPanelWidget chestGrid;

		// Token: 0x04000016 RID: 22
		private ButtonWidget ArrowRightButton;

		// Token: 0x04000017 RID: 23
		private ButtonWidget ArrowLeftButton;

		// Token: 0x04000018 RID: 24
		private int index;

		// Token: 0x04000019 RID: 25
		private ToolClass tools = new ToolClass();
	}
}
