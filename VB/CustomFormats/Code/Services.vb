Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.XtraScheduler.Services
Imports DevExpress.XtraScheduler.Drawing
Imports DevExpress.Web.ASPxScheduler.Services
Imports DevExpress.XtraScheduler

Public Class CustomTimeRulerFormatStringService
	Inherits TimeRulerFormatStringServiceWrapper
	Public Sub New(ByVal service As ITimeRulerFormatStringService)
		MyBase.New(service)
	End Sub

	Public Overrides Function GetHalfDayHourFormat(ByVal ruler As TimeRuler) As String
		If ruler.UseClientTimeZone Then
			Return "'HalfDayHour'"
		Else
			Return "HH:mm"
		End If
	End Function
	Public Overrides Function GetHourFormat(ByVal ruler As TimeRuler) As String
		If ruler.UseClientTimeZone Then
			Return "HH - 'Hour'"
		Else
			Return "HH:mm"
		End If
	End Function
	Public Overrides Function GetHourOnlyFormat(ByVal ruler As TimeRuler) As String
		If ruler.UseClientTimeZone Then
			Return "HH -> 'HourOnly'"
		Else
			Return "HH"
		End If
	End Function
	Public Overrides Function GetMinutesOnlyFormat(ByVal ruler As TimeRuler) As String
		If ruler.UseClientTimeZone Then
			Return "mm - 'MinutesOnly'"
		Else
			Return "mm"
		End If
	End Function
	Public Overrides Function GetTimeDesignatorOnlyFormat(ByVal ruler As TimeRuler) As String
		If ruler.UseClientTimeZone Then
			Return "tt - 'TimeDesignatorOnly'"
		Else
			Return "mm"
		End If
	End Function
End Class

Public Class CustomAppointmentFormatStringService
	Inherits AppointmentFormatStringServiceWrapper
	Public Sub New(ByVal service As IAppointmentFormatStringService)
		MyBase.New(service)
	End Sub
	Public Overrides Function GetVerticalAppointmentStartFormat(ByVal aptViewInfo As IAppointmentViewInfo) As String
		Return "{0: HHmm:ss}"
	End Function
	Public Overrides Function GetVerticalAppointmentEndFormat(ByVal aptViewInfo As IAppointmentViewInfo) As String
		Return "{0: HHmm:ss}"
	End Function
	Public Overrides Function GetHorizontalAppointmentEndFormat(ByVal aptViewInfo As IAppointmentViewInfo) As String
		Return "{0: HHmm}"
	End Function
	Public Overrides Function GetHorizontalAppointmentStartFormat(ByVal aptViewInfo As IAppointmentViewInfo) As String
		Return "{0: HHmm}"
	End Function
	Public Overrides Function GetContinueItemStartFormat(ByVal aptViewInfo As IAppointmentViewInfo) As String
		Return "{0: HHmm MMM dd}"
	End Function
	Public Overrides Function GetContinueItemEndFormat(ByVal aptViewInfo As IAppointmentViewInfo) As String
		Return "{0: HHmm MMM dd}"
	End Function
End Class
Public Class CustomHeaderCaptionService
	Inherits HeaderCaptionServiceWrapper
	Public Sub New(ByVal service As IHeaderCaptionService)
		MyBase.New(service)
	End Sub

	Public Overrides Function GetDayColumnHeaderCaption(ByVal header As DevExpress.Web.ASPxScheduler.Rendering.WebDateHeader) As String
		Dim [date] As DateTime = header.Interval.Start.Date
		If [date].Month = 1 AndAlso [date].Day = 1 Then
			Return "Happy New Year!"
		Else
			Return MyBase.GetDayColumnHeaderCaption(header)
		End If
	End Function


	Public Overrides Function GetDayOfWeekHeaderCaption(ByVal header As DevExpress.Web.ASPxScheduler.Rendering.WebDayOfWeekHeader) As String
		Dim [date] As DayOfWeek = header.DayOfWeek
		If [date] = DayOfWeek.Friday Then
			Return "TGIF!"
		Else
			Return MyBase.GetDayOfWeekHeaderCaption(header)
		End If
	End Function

	Public Overrides Function GetHorizontalWeekCellHeaderCaption(ByVal header As DevExpress.Web.ASPxScheduler.Rendering.WebDateCellHeader) As String
		Dim [date] As DateTime = header.Interval.Start.Date
		If [date].Month = 1 AndAlso [date].Day = 1 Then
			Return "HorizontalWeekCellHeader"
		Else
			Return [date].ToString("D")
		End If

	End Function

	Public Overrides Function GetTimeScaleHeaderCaption(ByVal header As DevExpress.Web.ASPxScheduler.Rendering.WebTimeScaleHeader) As String
		Dim [date] As DateTime = header.Interval.Start.Date
		If [date].Month = 1 AndAlso [date].Day = 1 Then
			Return "TimeScaleHeader"
		Else
			Return MyBase.GetTimeScaleHeaderCaption(header)
		End If

	End Function

	Public Overrides Function GetVerticalWeekCellHeaderCaption(ByVal header As DevExpress.Web.ASPxScheduler.Rendering.WebDateCellHeader) As String
		Dim [date] As DateTime = header.Interval.Start.Date
		If [date].Month = 1 AndAlso [date].Day = 1 Then
			Return "VerticalWeekCellHeader"
		Else
			Return MyBase.GetVerticalWeekCellHeaderCaption(header)
		End If

	End Function
End Class

Public Class CustomHeaderToolTipService
	Inherits HeaderToolTipServiceWrapper
	Public Sub New(ByVal service As IHeaderToolTipService)
		MyBase.New(service)
	End Sub

	Public Overrides Function GetDayColumnHeaderToolTip(ByVal header As DevExpress.Web.ASPxScheduler.Rendering.WebDateHeader) As String
		Dim [date] As DateTime = header.Interval.Start.Date
		If [date].Month = 1 AndAlso [date].Day = 1 Then
			Return "Let's celebrate!"
		Else
			Return MyBase.GetDayColumnHeaderToolTip(header)
		End If
	End Function

	Public Overrides Function GetDayOfWeekHeaderToolTip(ByVal header As DevExpress.Web.ASPxScheduler.Rendering.WebDayOfWeekHeader) As String
		If header.DayOfWeek = DayOfWeek.Friday Then
			Return "TGIF!"
		Else
			Return MyBase.GetDayOfWeekHeaderToolTip(header)
		End If
	End Function

	Public Overrides Function GetTimeScaleHeaderToolTip(ByVal header As DevExpress.Web.ASPxScheduler.Rendering.WebTimeScaleHeader) As String
		If header.Interval.Contains(New DateTime(2009, 1, 1)) Then
			Return "Let's celebrate!"
		Else
			Return MyBase.GetTimeScaleHeaderToolTip(header)
		End If
	End Function
	Public Overrides Function GetHorizontalWeekCellHeaderToolTip(ByVal header As DevExpress.Web.ASPxScheduler.Rendering.WebDateCellHeader) As String
		If header.Interval.IntersectsWith(New TimeInterval(New DateTime(2008, 12, 24), New DateTime(2009, 1, 4))) Then
			Return "Merry Christmas and Happy New Year!"
		Else
		Return MyBase.GetHorizontalWeekCellHeaderToolTip(header)
		End If
	End Function
	Public Overrides Function GetVerticalWeekCellHeaderToolTip(ByVal header As DevExpress.Web.ASPxScheduler.Rendering.WebDateCellHeader) As String
		If header.Interval.IntersectsWith(New TimeInterval(New DateTime(2008, 12, 24), New DateTime(2009, 1, 4))) Then
			Return "Merry Christmas and Happy New Year!"
		Else
		Return MyBase.GetVerticalWeekCellHeaderToolTip(header)
		End If
	End Function
End Class