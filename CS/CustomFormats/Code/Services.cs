using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.XtraScheduler.Services;
using DevExpress.XtraScheduler.Drawing;
using DevExpress.Web.ASPxScheduler.Services;
using DevExpress.XtraScheduler;

public class CustomTimeRulerFormatStringService : TimeRulerFormatStringServiceWrapper {
    public CustomTimeRulerFormatStringService(ITimeRulerFormatStringService service)
        : base(service) {
    }

    public override string GetHalfDayHourFormat(TimeRuler ruler) {
        return ruler.UseClientTimeZone ? "'HalfDayHour'" : "HH:mm";
    }
    public override string GetHourFormat(TimeRuler ruler) {
        return ruler.UseClientTimeZone ? "HH - 'Hour'" : "HH:mm";
    }
    public override string GetHourOnlyFormat(TimeRuler ruler) {
        return ruler.UseClientTimeZone ? "HH -> 'HourOnly'" : "HH";
    }
    public override string GetMinutesOnlyFormat(TimeRuler ruler) {
        return ruler.UseClientTimeZone ? "mm - 'MinutesOnly'" : "mm";
    }
    public override string GetTimeDesignatorOnlyFormat(TimeRuler ruler) {
        return ruler.UseClientTimeZone ? "tt - 'TimeDesignatorOnly'" : "mm";
    }
}

public class CustomAppointmentFormatStringService : AppointmentFormatStringServiceWrapper {
    public CustomAppointmentFormatStringService(IAppointmentFormatStringService service)
        : base(service) {
    }
    public override string GetVerticalAppointmentStartFormat(IAppointmentViewInfo aptViewInfo) {
        return "{0: HHmm:ss}";
    }
    public override string GetVerticalAppointmentEndFormat(IAppointmentViewInfo aptViewInfo) {
        return "{0: HHmm:ss}";
    }
    public override string GetHorizontalAppointmentEndFormat(IAppointmentViewInfo aptViewInfo) {
        return "{0: HHmm}";
    }
    public override string GetHorizontalAppointmentStartFormat(IAppointmentViewInfo aptViewInfo) {
        return "{0: HHmm}";
    }
    public override string GetContinueItemStartFormat(IAppointmentViewInfo aptViewInfo) {
        return "{0: HHmm MMM dd}";
    }
    public override string GetContinueItemEndFormat(IAppointmentViewInfo aptViewInfo) {
        return "{0: HHmm MMM dd}";
    }
}
public class CustomHeaderCaptionService : HeaderCaptionServiceWrapper {
    public CustomHeaderCaptionService(IHeaderCaptionService service)
        : base(service) {
    }

    public override string GetDayColumnHeaderCaption(DevExpress.Web.ASPxScheduler.Rendering.WebDateHeader header) {
        DateTime date = header.Interval.Start.Date;
        if (date.Month == 1 && date.Day == 1)
            return "Happy New Year!";
        else
            return base.GetDayColumnHeaderCaption(header);
    }


    public override string GetDayOfWeekHeaderCaption(DevExpress.Web.ASPxScheduler.Rendering.WebDayOfWeekHeader header) {
        DayOfWeek date = header.DayOfWeek;
        if (date == DayOfWeek.Friday)
            return "TGIF!";
        else
            return base.GetDayOfWeekHeaderCaption(header);
    }

    public override string GetHorizontalWeekCellHeaderCaption(DevExpress.Web.ASPxScheduler.Rendering.WebDateCellHeader header) {
        DateTime date = header.Interval.Start.Date;
        if (date.Month == 1 && date.Day == 1)
            return "HorizontalWeekCellHeader";
        else
            return date.ToString("D");

    }
   
    public override string GetTimeScaleHeaderCaption(DevExpress.Web.ASPxScheduler.Rendering.WebTimeScaleHeader header) {
        DateTime date = header.Interval.Start.Date;
        if (date.Month == 1 && date.Day == 1)
            return "TimeScaleHeader";
        else
            return base.GetTimeScaleHeaderCaption(header);

    }

    public override string GetVerticalWeekCellHeaderCaption(DevExpress.Web.ASPxScheduler.Rendering.WebDateCellHeader header) {
        DateTime date = header.Interval.Start.Date;
        if (date.Month == 1 && date.Day == 1)
            return "VerticalWeekCellHeader";
        else
            return base.GetVerticalWeekCellHeaderCaption(header);

    }
}

public class CustomHeaderToolTipService : HeaderToolTipServiceWrapper {
    public CustomHeaderToolTipService(IHeaderToolTipService service)
        : base(service) {
    }
  
    public override string GetDayColumnHeaderToolTip(DevExpress.Web.ASPxScheduler.Rendering.WebDateHeader header) {
        DateTime date = header.Interval.Start.Date;
        if (date.Month == 1 && date.Day == 1)
            return "Let's celebrate!";
        else
            return base.GetDayColumnHeaderToolTip(header);
    }
    
    public override string GetDayOfWeekHeaderToolTip(DevExpress.Web.ASPxScheduler.Rendering.WebDayOfWeekHeader header) {
        if (header.DayOfWeek == DayOfWeek.Friday)
            return "TGIF!";
        else
            return base.GetDayOfWeekHeaderToolTip(header);
    }

    public override string GetTimeScaleHeaderToolTip(DevExpress.Web.ASPxScheduler.Rendering.WebTimeScaleHeader header) {
        if (header.Interval.Contains(new DateTime(2009, 1, 1)))
            return "Let's celebrate!";
        else
            return base.GetTimeScaleHeaderToolTip(header);
    }
    public override string GetHorizontalWeekCellHeaderToolTip(DevExpress.Web.ASPxScheduler.Rendering.WebDateCellHeader header) {
        if (header.Interval.IntersectsWith(new TimeInterval(new DateTime(2008, 12, 24), new DateTime(2009, 1, 4))))
            return "Merry Christmas and Happy New Year!";
        else
        return base.GetHorizontalWeekCellHeaderToolTip(header);
    }
    public override string GetVerticalWeekCellHeaderToolTip(DevExpress.Web.ASPxScheduler.Rendering.WebDateCellHeader header) {
        if (header.Interval.IntersectsWith(new TimeInterval(new DateTime(2008, 12, 24), new DateTime(2009, 1, 4))))
            return "Merry Christmas and Happy New Year!";
        else
        return base.GetVerticalWeekCellHeaderToolTip(header);
    }
}