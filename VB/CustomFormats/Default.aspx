<%@ Page Language="vb" AutoEventWireup="true" Inherits="_Default" Codebehind="Default.aspx.vb" %>

<%@ Register Assembly="DevExpress.Web.v8.3, Version=8.3.2.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dxrp" %>

<%@ Register Assembly="DevExpress.Web.v8.3, Version=8.3.2.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v8.3, Version=8.3.2.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<%@ Register assembly="DevExpress.Web.ASPxScheduler.v8.3, Version=8.3.2.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxScheduler" tagprefix="dxwschs" %>
<%@ Register assembly="DevExpress.XtraScheduler.v8.3.Core, Version=8.3.2.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraScheduler" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<form id="form1" runat="server">
	<div>

	</div>
		<dxp:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%">
			<PanelCollection>
				<dxp:PanelContent runat="server" _designerRegion="0">
					&nbsp;&nbsp;&nbsp; &nbsp;
					<br />
		<dxwschs:ASPxScheduler id="ASPxScheduler1" runat="server" Width="100%" 
		OnAppointmentFormShowing="ASPxScheduler1_AppointmentFormShowing" GroupType="Resource" Start="2009-01-01" ClientInstanceName="scheduler" OnAppointmentInserting="ASPxScheduler1_AppointmentInserting" OnBeforeExecuteCallbackCommand="ASPxScheduler1_BeforeExecuteCallbackCommand">
		<Storage>
			<Appointments>
				<Mappings AllDay="AllDay" AppointmentId="Id" Description="Description" 
					End="End" Label="BranchId" Location="Location" RecurrenceInfo="RecurrenceInfo" 
					ReminderInfo="ReminderInfo" ResourceId="ResourceId" Start="Start" 
					Status="Status" Subject="Subject" Type="type" />
				<CustomFieldMappings>
					<dxwschs:ASPxAppointmentCustomFieldMapping Member="Price" Name="Price" />
					<dxwschs:ASPxAppointmentCustomFieldMapping Name="ContactInfo" Member="ContactInfo" />
				</CustomFieldMappings>
			</Appointments>
		</Storage>            
		<OptionsForms AppointmentFormTemplateUrl="~/MyForms/MyAppointmentForm.ascx" />
		<Views>
			<DayView ResourcesPerPage="1" DayCount="3"><TimeRulers><cc1:TimeRuler UseClientTimeZone="False" ShowMinutes="True" >
				<timezone id="Greenwich"></timezone>
			</cc1:TimeRuler>
				<cc1:TimeRuler ShowMinutes="True">
				</cc1:TimeRuler>
			</TimeRulers>
				<AppointmentDisplayOptions EndTimeVisibility="Always" StartTimeVisibility="Always"
					TimeDisplayType="Text" />
				<DayViewStyles>
					<TimeRulerHoursItem Font-Size="Small" Width="100px">
					</TimeRulerHoursItem>
					<TimeRulerMinuteItem Font-Size="XX-Small" Width="75px">
					</TimeRulerMinuteItem>
				</DayViewStyles>
			</DayView>
			<WorkWeekView NavigationButtonVisibility="Always" ResourcesPerPage="1"><TimeRulers><cc1:TimeRuler />
				<cc1:TimeRuler UseClientTimeZone="False">
					<timezone id="Greenwich"></timezone>
				</cc1:TimeRuler>
			</TimeRulers></WorkWeekView>
			<WeekView ResourcesPerPage="3">
			</WeekView>
			<MonthView ResourcesPerPage="1">
			</MonthView>
		</Views>
			<OptionsBehavior RemindersFormDefaultAction="Custom" ShowRemindersForm="False" />
	</dxwschs:ASPxScheduler>
				</dxp:PanelContent>
			</PanelCollection>
		</dxp:ASPxPanel>
		&nbsp;
<asp:ObjectDataSource ID="appointmentDataSource" runat="server" DataObjectTypeName="CustomEvent" TypeName="CustomEventDataSource" DeleteMethod="DeleteMethodHandler" SelectMethod="SelectMethodHandler" InsertMethod="InsertMethodHandler" UpdateMethod="UpdateMethodHandler" OnObjectCreated="appointmentsDataSource_ObjectCreated" />

	</form>
	<script type="text/javascript">
	function OnBtnCreateAptRemClick() {
		scheduler.RaiseCallback("CREATAPTWR|");
	}
	</script>

</body>
</html>
