using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Model.EventService;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryService;
using Es.Udc.DotNet.ModelUtil.IoC;
using System.Reflection;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
using System.Data.SqlClient;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Event
{
    public partial class EventsFounded : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
            lblNoEvents.Visible = false;
            lnkPrevious.Visible = false;
            int startIndex, count;
            lblNoEvents.Visible = false;

            /* Get Keywords */
            String words = Convert.ToString(Request.Params.Get("words"));

            /* Get the category (without time) */
            long? categoryId;
            try
            {
                categoryId = Convert.ToInt64(Request.Params.Get("categoryId"));
            }
            catch (FormatException)
            {
                categoryId = null;
            }

            /* Get Start Index */
            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }

            /* Get Count */
            try
            {
                count = Int32.Parse(Request.Params.Get("count"));
            }
            catch (ArgumentNullException)
            {
                count = 10;
            }

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IEventService eventService = iocManager.Resolve<IEventService>();

            /* Get Events Info */
            EventWithExist events = eventService.FindEvents(words, categoryId, startIndex, count);

            if (events.events.Count == 0)
            {
                lblNoEvents.Visible = true;
                return;
            }

            gvEvents.DataSource = events.events;
            gvEvents.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url = "~/Pages/Event/EventsFounded.aspx?words=" + words + "&categoryId=" + categoryId + "&startIndex=" + (startIndex - count) + "&count=" + count;

                this.lnkPrevious.NavigateUrl = Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (events.exist)
            {
                String url = "~/Pages/Event/EventsFounded.aspx?words=" + words + "&categoryId=" + categoryId + "&startIndex=" + (startIndex + count) + "&count=" + count;

                this.lnkNext.NavigateUrl = Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }
        }

        protected void LinkButton_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "comentarios")
            {
                long eventId = Convert.ToInt64(e.CommandArgument);

                // Obtener el servicio de eventos
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IEventService eventService = iocManager.Resolve<IEventService>();

                // Obtener el evento por su ID
                EventTable eventTable = eventService.FindByEventId(eventId);

                if (eventTable.hasComments)
                {
                    String url = String.Format("~/Pages/Comment/AllComments.aspx?eventId={0}", eventId);
                    Response.Redirect(Response.ApplyAppPathModifier(url));
                }
                else
                {
                    lblError.Visible = true;
                }
            }
            else if (e.CommandName == "comentar" || e.CommandName == "recomendar")
            {
                if (!SessionManager.IsUserAuthenticated(Context))
                {
                    lblError.Text = "Debe autenticarse para realizar esta acción.";
                    lblError.Visible = true;
                }
                else
                {
                    // Lógica para comentar o recomendar
                    if (e.CommandName == "comentar")
                    {
                        long eventId = Convert.ToInt64(e.CommandArgument);
                        String url = String.Format("~/Pages/Comment/AddComment.aspx?eventId={0}", eventId);
                        Response.Redirect(Response.ApplyAppPathModifier(url));
                    }
                    else if (e.CommandName == "recomendar")
                    {
                        long eventId = Convert.ToInt64(e.CommandArgument);
                        String url = String.Format("~/Pages/Recommendations/Recommend.aspx?eventId={0}", eventId);
                        Response.Redirect(Response.ApplyAppPathModifier(url));
                    }
                }
            }
        }

        protected void gvEvents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "comentarios")
            {
                long eventId = Convert.ToInt64(e.CommandArgument);

                // Obtener el servicio de eventos
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IEventService eventService = iocManager.Resolve<IEventService>();

                // Obtener el evento por su ID
                EventTable eventTable = eventService.FindByEventId(eventId);

                if (eventTable.hasComments)
                {
                    String url = String.Format("~/Pages/Comment/AllComments.aspx?eventId={0}", eventId);
                    Response.Redirect(Response.ApplyAppPathModifier(url));
                }
                else
                {
                    lblError.Visible = true;
                }
            }
            else if (e.CommandName == "comentar" || e.CommandName == "recomendar")
            {
                if (!SessionManager.IsUserAuthenticated(Context))
                {
                    lblError.Visible = true;
                }
                else
                {
                    // Lógica para comentar o recomendar
                    if (e.CommandName == "comentar")
                    {
                        long eventId = Convert.ToInt64(e.CommandArgument);
                        String url = String.Format("~/Pages/Comment/AddComment.aspx?eventId={0}", eventId);
                        Response.Redirect(Response.ApplyAppPathModifier(url));
                    }
                    else if (e.CommandName == "recomendar")
                    {
                        long eventId = Convert.ToInt64(e.CommandArgument);
                        String url = String.Format("~/Pages/Recommendations/Recommend.aspx?eventId={0}", eventId);
                        Response.Redirect(Response.ApplyAppPathModifier(url));
                    }
                }
            }
        }
    }
}