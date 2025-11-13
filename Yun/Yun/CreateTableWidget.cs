using System;
using System.Xml.Linq;
using Engine;
using Game;

namespace Yun
{
	// Token: 0x02000002 RID: 2
	public class CreateTableWidget : CanvasWidget
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public CreateTableWidget(ComponentChest chest, ComponentCraftingTable componentCraftingTable, int index, ComponentPlayer player)
		{
			this.index = index;
			this.player = player;
			this.entities = player.Project.FindSubsystem<SubsystemBlockEntities>(true);
			this.m_componentCraftingTable = componentCraftingTable;
			XElement xelement = ContentManager.Get<XElement>("Widgets/CreateTableWidget", null);
			base.LoadContents(this, xelement);
			this.YunChest = this.Children.Find<GridPanelWidget>("ChestGrid", true);
			this.m_craftingGrid = this.Children.Find<GridPanelWidget>("CraftingGrid", true);
			this.ArrowLeftButton = this.Children.Find<ButtonWidget>("ArrowLeftButton", true);
			this.ArrowRightButton = this.Children.Find<ButtonWidget>("ArrowRightButton", true);
			this.m_craftingResultSlot = this.Children.Find<InventorySlotWidget>("CraftingResultSlot", true);
			this.m_craftingRemainsSlot = this.Children.Find<InventorySlotWidget>("CraftingRemainsSlot", true);
			int num = 0;
			for (int i = 0; i < this.m_craftingGrid.RowsCount; i++)
			{
				for (int j = 0; j < this.m_craftingGrid.ColumnsCount; j++)
				{
					InventorySlotWidget inventorySlotWidget = new InventorySlotWidget();
					inventorySlotWidget.AssignInventorySlot(componentCraftingTable, num++);
					this.m_craftingGrid.Children.Add(inventorySlotWidget);
					this.m_craftingGrid.SetWidgetCell(inventorySlotWidget, new Point2(j, i));
				}
			}
			num = 0;
			for (int k = 0; k < this.YunChest.RowsCount; k++)
			{
				for (int l = 0; l < this.YunChest.ColumnsCount; l++)
				{
					InventorySlotWidget inventorySlotWidget2 = new InventorySlotWidget
					{
						Size = new Vector2(53f, 53f)
					};
					inventorySlotWidget2.AssignInventorySlot(chest, num++);
					this.YunChest.Children.Add(inventorySlotWidget2);
					this.YunChest.SetWidgetCell(inventorySlotWidget2, new Point2(l, k));
				}
			}
			this.m_craftingResultSlot.AssignInventorySlot(this.m_componentCraftingTable, this.m_componentCraftingTable.ResultSlotIndex);
			this.m_craftingRemainsSlot.AssignInventorySlot(this.m_componentCraftingTable, this.m_componentCraftingTable.RemainsSlotIndex);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002294 File Offset: 0x00000494
		private void Switch(int index)
		{
			bool flag = this.entities.GetBlockEntity(0, index, 1) == null;
			bool flag2 = flag;
			if (flag2)
			{
				this.tools.YunUp(index);
			}
			else
			{
				this.player.ComponentGui.ModalPanelWidget = new CreateTableWidget(this.entities.GetBlockEntity(0, index, 1).Entity.FindComponent<ComponentChest>(true), this.entities.GetBlockEntity(1, 1, 1).Entity.FindComponent<ComponentCraftingTable>(true), index, this.player);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000231C File Offset: 0x0000051C
		public override void Update()
		{
			this.ArrowLeftButton.IsEnabled = (this.index > 0);
			this.ArrowRightButton.IsEnabled = (this.index < 3);
			bool isClicked = this.ArrowRightButton.IsClicked;
			bool flag = isClicked;
			if (flag)
			{
				this.Switch(this.index + 1);
			}
			bool isClicked2 = this.ArrowLeftButton.IsClicked;
			bool flag2 = isClicked2;
			if (flag2)
			{
				this.Switch(this.index - 1);
			}
		}

		// Token: 0x04000001 RID: 1
		public GridPanelWidget YunChest;

		// Token: 0x04000002 RID: 2
		public GridPanelWidget m_craftingGrid;

		// Token: 0x04000003 RID: 3
		public InventorySlotWidget m_craftingResultSlot;

		// Token: 0x04000004 RID: 4
		public InventorySlotWidget m_craftingRemainsSlot;

		// Token: 0x04000005 RID: 5
		public ComponentCraftingTable m_componentCraftingTable;

		// Token: 0x04000006 RID: 6
		private ButtonWidget ArrowRightButton;

		// Token: 0x04000007 RID: 7
		private ButtonWidget ArrowLeftButton;

		// Token: 0x04000008 RID: 8
		private SubsystemBlockEntities entities;

		// Token: 0x04000009 RID: 9
		private ToolClass tools = new ToolClass();

		// Token: 0x0400000A RID: 10
		private ComponentPlayer player;

		// Token: 0x0400000B RID: 11
		private int index;
	}
}
