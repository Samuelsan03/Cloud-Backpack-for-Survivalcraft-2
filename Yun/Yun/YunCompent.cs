using System;
using Engine;
using Engine.Media;
using Game;
using GameEntitySystem;
using TemplatesDatabase;

namespace Yun
{
	// Token: 0x02000004 RID: 4
	public class YunCompent : ComponentInventoryBase, IUpdateable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002520 File Offset: 0x00000720
		public UpdateOrder UpdateOrder
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002534 File Offset: 0x00000734
		public override void Load(ValuesDictionary valuesDictionary, IdToEntityMap idToEntityMap)
		{
			this.Player = base.Entity.FindComponent<ComponentPlayer>();
			try
			{
				this.YButton = this.Player.GuiWidget.Children.Find<ButtonWidget>("YButton", true);
				this.GButton = this.Player.GuiWidget.Children.Find<ButtonWidget>("GButton", true);
			}
			catch
			{
				this.YButton = new BitmapButtonWidget
				{
					Name = "YButton",
					Font = ContentManager.Get<BitmapFont>("Fonts/Pericles", null),
					Size = new Vector2(68f, 64f),
					Margin = new Vector2(4f, 0f),
					NormalSubtexture = ContentManager.Get<Subtexture>("Textures/YButton", null),
					ClickedSubtexture = ContentManager.Get<Subtexture>("Textures/YButtonPress", null),
					IsEnabled = true
				};
				this.GButton = new BitmapButtonWidget
				{
					Name = "GButton",
					Font = ContentManager.Get<BitmapFont>("Fonts/Pericles", null),
					Size = new Vector2(64f, 64f),
					Margin = new Vector2(0f, 3f),
					NormalSubtexture = ContentManager.Get<Subtexture>("Textures/GButton", null),
					ClickedSubtexture = ContentManager.Get<Subtexture>("Textures/GButtonPress", null),
					HorizontalAlignment = (WidgetAlignment)2,
					IsEnabled = true
				};
				this.Player.GuiWidget.Children.Find<StackPanelWidget>("MoreContents", true).Children.Add(this.YButton);
				this.Player.GuiWidget.Children.Find<StackPanelWidget>("RightControlsContainer", true).Children.Add(this.GButton);
			}
			this.Entities = base.Project.FindSubsystem<SubsystemBlockEntities>();
			base.Load(valuesDictionary, idToEntityMap);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002740 File Offset: 0x00000940
		void IUpdateable.Update(float dt)
		{
			bool isClicked = this.YButton.IsClicked;
			bool flag = isClicked;
			if (flag)
			{
				bool flag2 = this.Player.ComponentGui.ModalPanelWidget is YunWidget;
				bool flag3 = flag2;
				if (flag3)
				{
					this.Player.ComponentGui.ModalPanelWidget = null;
				}
				else
				{
					bool flag4 = this.Entities.GetBlockEntity(0, 0, 1) == null;
					bool flag5 = flag4;
					if (flag5)
					{
						this.Tools.YunUp(0);
					}
					else
					{
						this.Player.ComponentGui.ModalPanelWidget = new YunWidget(this.Player, this.Entities.GetBlockEntity(0, 0, 1).Entity.FindComponent<ComponentChest>(), 0);
					}
				}
			}
			bool isClicked2 = this.GButton.IsClicked;
			bool flag6 = isClicked2;
			if (flag6)
			{
				bool flag7 = this.Entities.GetBlockEntity(0, 0, 1) == null;
				bool flag8 = flag7;
				if (flag8)
				{
					this.Tools.YunUp(0);
				}
				else
				{
					bool flag9 = this.Player.ComponentGui.ModalPanelWidget is CreateTableWidget;
					bool flag10 = flag9;
					if (flag10)
					{
						this.Player.ComponentGui.ModalPanelWidget = null;
					}
					else
					{
						bool flag11 = this.Entities.GetBlockEntity(1, 1, 1) == null;
						bool flag12 = flag11;
						if (flag12)
						{
							this.Tools.CreateUp();
						}
						else
						{
							this.Player.ComponentGui.ModalPanelWidget = new CreateTableWidget(this.Entities.GetBlockEntity(0, 0, 1).Entity.FindComponent<ComponentChest>(), this.Entities.GetBlockEntity(1, 1, 1).Entity.FindComponent<ComponentCraftingTable>(true), 0, this.Player);
						}
					}
				}
			}
		}

		// Token: 0x0400000C RID: 12
		public ComponentPlayer Player;

		// Token: 0x0400000D RID: 13
		public ButtonWidget YButton;

		// Token: 0x0400000E RID: 14
		public SubsystemBlockEntities Entities;

		// Token: 0x0400000F RID: 15
		public ToolClass Tools = new ToolClass();

		// Token: 0x04000010 RID: 16
		public ButtonWidget GButton;
	}
}
