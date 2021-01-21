﻿using JocysCom.ClassLibrary.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using x360ce.Engine;
using x360ce.Engine.Data;

namespace x360ce.App.Controls
{
	/// <summary>
	/// Interaction logic for PresetsControl.xaml
	/// </summary>
	public partial class PresetsControl : UserControl
	{
		public PresetsControl()
		{
			InitializeComponent();
		}

		public BaseWithHeaderControl _ParentControl;

		public void InitForm()
		{
			//_ParentForm.SetImage(SetHeaderBody(MessageBoxIcon.None);
			//SettingsGridPanel._ParentForm = _ParentForm;
			//SummariesGridPanel._ParentForm = _ParentForm;
			PresetsGridPanel._ParentForm = _ParentControl;
			//SettingsGridPanel.InitPanel();
			//SummariesGridPanel.InitPanel();
			PresetsGridPanel.InitPanel();
			UpdateControls();
			_ParentControl.Button1.Click += OkButton_Click;
		}

		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			Guid? checksum = null;
			if (MainTabControl.SelectedItem == PresetsTabPage)
			{
				var preset = PresetsGridPanel.MainDataGrid.SelectedItems.Cast<Preset>().FirstOrDefault();
				if (preset != null)
					checksum = preset.PadSettingChecksum;
			}
			//if (MainTabControl.SelectedItem == SummariesTabPage)
			//{
			//	var row = SummariesGridPanel.SummariesDataGridView.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
			//	if (row != null)
			//	{
			//		var summary = (Summary)row.DataBoundItem;
			//		checksum = summary.PadSettingChecksum;
			//	}
			//}
			//if (MainTabControl.SelectedItem == SettingsTabPage)
			//{
			//	var row = SettingsGridPanel.SettingsDataGridView.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
			//	if (row != null)
			//	{
			//		var setting = (UserSetting)row.DataBoundItem;
			//		checksum = setting.PadSettingChecksum;
			//	}
			//}
			if (checksum.HasValue)
			{
				SelectedItem = SettingsManager.PadSettings.Items.FirstOrDefault(x => x.PadSettingChecksum == checksum.Value);
			}
		}

		public void UnInitForm()
		{
			//SettingsGridPanel.UnInitPanel();
			//SummariesGridPanel.UnInitPanel();
			PresetsGridPanel.UnInitPanel();
		}

		public PadSetting SelectedItem;


		private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			UpdateControls();
		}

		private void UpdateControls()
		{
			var tab = MainTabControl.SelectedItem as TabItem;
			if (tab != null)
				_ParentControl.SetHead(tab.Header as string);
			var selected = false;
			//if (MainTabControl.SelectedItem == SummariesTabPage)
			//	selected = SummariesGridPanel.SummariesDataGridView.Rows.Count > 0;
			//if (MainTabControl.SelectedItem == SettingsTabPage)
			//	selected = SettingsGridPanel.SettingsDataGridView.Rows.Count > 0;
			if (MainTabControl.SelectedItem == PresetsTabPage)
				selected = PresetsGridPanel.MainDataGrid.SelectedItems.Count > 0;
			ControlsHelper.SetEnabled(_ParentControl.Button1, selected);
		}

	}
}
