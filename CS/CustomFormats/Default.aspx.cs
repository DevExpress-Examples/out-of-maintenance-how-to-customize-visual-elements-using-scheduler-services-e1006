using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;
using DevExpress.Web.ASPxScheduler.Internal;
using DevExpress.XtraScheduler.Services;
using DevExpress.Web.ASPxScheduler.Services;

public partial class _Default : System.Web.UI.Page {
    ASPxSchedulerStorage Storage { get { return this.ASPxScheduler1.Storage; } }
    public static Random RandomInstance = new Random();

    ITimeRulerFormatStringService prevTimeRulerFormatStringService;
    CustomTimeRulerFormatStringService customTimeRulerFormatStringService;
    IAppointmentFormatStringService prevAppointmentFormatStringService;
    CustomAppointmentFormatStringService customAppointmentFormatStringService;
    IHeaderCaptionService prevHeaderCaptionService;
    CustomHeaderCaptionService customHeaderCaptionService;
    IHeaderToolTipService prevHeaderToolTipService;
    CustomHeaderToolTipService customHeaderToolTipService;



    protected override void OnInit(EventArgs e) {
        base.OnInit(e);
        CreateAppointmentFormatStringService();
        CreateTimeRulerFormatStringService();
        CreateHeaderCaptionService();
        CreateHeaderToolTipService();


        ASPxScheduler1.RemoveService(typeof(IAppointmentFormatStringService));
        ASPxScheduler1.AddService(typeof(IAppointmentFormatStringService), customAppointmentFormatStringService);

        ASPxScheduler1.RemoveService(typeof(ITimeRulerFormatStringService));
        ASPxScheduler1.AddService(typeof(ITimeRulerFormatStringService), customTimeRulerFormatStringService);

        ASPxScheduler1.RemoveService(typeof(IHeaderCaptionService));
        ASPxScheduler1.AddService(typeof(IHeaderCaptionService), customHeaderCaptionService);

        ASPxScheduler1.RemoveService(typeof(IHeaderToolTipService));
        ASPxScheduler1.AddService(typeof(IHeaderToolTipService), customHeaderToolTipService);

    }
    protected void Page_Load(object sender, EventArgs e) {

        SetupMappings();
        ResourceFiller.FillResources(this.ASPxScheduler1.Storage, 3);

        ASPxScheduler1.AppointmentDataSource = appointmentDataSource;
        ASPxScheduler1.DataBind();
        

    }
    #region Service Creation
    public void CreateAppointmentFormatStringService() {
        this.prevAppointmentFormatStringService = (IAppointmentFormatStringService)ASPxScheduler1.GetService(typeof(IAppointmentFormatStringService));
        this.customAppointmentFormatStringService = new CustomAppointmentFormatStringService(prevAppointmentFormatStringService);
    }
    public void CreateTimeRulerFormatStringService() {
        this.prevTimeRulerFormatStringService = (ITimeRulerFormatStringService)ASPxScheduler1.GetService(typeof(ITimeRulerFormatStringService));
        this.customTimeRulerFormatStringService = new CustomTimeRulerFormatStringService(prevTimeRulerFormatStringService);
    }
    public void CreateHeaderCaptionService() {
        this.prevHeaderCaptionService = (IHeaderCaptionService)ASPxScheduler1.GetService(typeof(IHeaderCaptionService));
        this.customHeaderCaptionService = new CustomHeaderCaptionService(prevHeaderCaptionService);
    }
    public void CreateHeaderToolTipService() {
        this.prevHeaderToolTipService = (IHeaderToolTipService)ASPxScheduler1.GetService(typeof(IHeaderToolTipService));
        this.customHeaderToolTipService = new CustomHeaderToolTipService(prevHeaderToolTipService);
    }
    #endregion



    #region Common Procedures
    #region Data Fill


    CustomEventList GetCustomEvents() {
        CustomEventList events = Session["ListBoundModeObjects"] as CustomEventList;
        if (events == null) {
            events = GenerateCustomEventList();
            Session["ListBoundModeObjects"] = events;
        }
        return events;
    }

    #region Random events generation
    CustomEventList GenerateCustomEventList() {
        CustomEventList eventList = new CustomEventList();
        int count = Storage.Resources.Count;
        for (int i = 0; i < count; i++) {
            Resource resource = Storage.Resources[i];
            string subjPrefix = resource.Caption + "'s ";

            eventList.Add(CreateEvent(resource.Id, subjPrefix + "meeting", 2, 5));
            eventList.Add(CreateEvent(resource.Id, subjPrefix + "travel", 3, 6));
            eventList.Add(CreateEvent(resource.Id, subjPrefix + "phone call", 0, 10));
        }
        return eventList;
    }
    CustomEvent CreateEvent(object resourceId, string subject, int status, int label) {
        CustomEvent customEvent = new CustomEvent();
        customEvent.Subject = subject;
        customEvent.OwnerId = resourceId;
        Random rnd = RandomInstance;
        int rangeInHours = 48;
        customEvent.StartTime = DateTime.Today + TimeSpan.FromHours(rnd.Next(0, rangeInHours));
        customEvent.EndTime = customEvent.StartTime + TimeSpan.FromHours(rnd.Next(0, rangeInHours / 8));
        customEvent.Status = status;
        customEvent.Label = label;
        customEvent.Id = "ev" + customEvent.GetHashCode();
        return customEvent;
    }
    #endregion


    void SetupMappings() {
        ASPxAppointmentMappingInfo mappings = Storage.Appointments.Mappings;
        Storage.BeginUpdate();
        try {
            mappings.AppointmentId = "Id";
            mappings.Start = "StartTime";
            mappings.End = "EndTime";
            mappings.Subject = "Subject";
            mappings.AllDay = "AllDay";
            mappings.Description = "Description";
            mappings.Label = "Label";
            mappings.Location = "Location";
            mappings.RecurrenceInfo = "RecurrenceInfo";
            mappings.ReminderInfo = "ReminderInfo";
            mappings.ResourceId = "OwnerId";
            mappings.Status = "Status";
            mappings.Type = "EventType";
        }
        finally {
            Storage.EndUpdate();
        }
    }
    #endregion


    protected void ASPxScheduler1_AppointmentFormShowing(object sender, AppointmentFormEventArgs e) {
        e.Container = new MyAppointmentFormTemplateContainer((ASPxScheduler)sender);

    }

    protected void ASPxScheduler1_AppointmentInserting(object sender, PersistentObjectCancelEventArgs e) {
        ASPxSchedulerStorage storage = (ASPxSchedulerStorage)sender;
        Appointment apt = (Appointment)e.Object;
        storage.SetAppointmentId(apt, "a" + apt.GetHashCode());
    }

    protected void appointmentsDataSource_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        e.ObjectInstance = new CustomEventDataSource(GetCustomEvents());
    }
    #endregion

    protected void ASPxScheduler1_BeforeExecuteCallbackCommand(object sender, SchedulerCallbackCommandEventArgs e) {
        if(e.CommandId == SchedulerCallbackCommandId.AppointmentSave)
            e.Command = new MyAppointmentSaveCallbackCommand((ASPxScheduler)sender);
    }
    
}
