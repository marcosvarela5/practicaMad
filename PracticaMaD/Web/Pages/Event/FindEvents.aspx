<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PracticaMaD.Master" CodeBehind="FindEvents.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Event.FindEvents" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="FindEventsForm" method="post" runat="server">
            <div class="field">
                <span class="label">
                    <asp:Label ID="lblKeyword" runat="server" meta:resourcekey="lblKeyword" />
                    <asp:Localize ID="lclKeywords" runat="server" meta:resourcekey="lclKeywords" />
                </span>
                <span class="entry">
                    <asp:TextBox ID="txtKeys" CssClass="textbox" runat="server" Width="100px" Columns="16" meta:resourcekey="txtKeys"></asp:TextBox>
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Label ID="lblCategory" runat="server" meta:resourcekey="lblCategory" />
                    <asp:Localize ID="lclCategory" runat="server" meta:resourcekey="lclCategory" />
                </span>
                <span class="entry">
                    <asp:DropDownList ID="dropCategory" runat="server" Width="110px" meta:resourcekey="dropCategory"></asp:DropDownList>
                </span>
            </div>

            <div class="button">
                <asp:Button CssClass="btn" ID="btnFindEvents" runat="server" OnClick="BtnFindEventsClick" meta:resourcekey="btnFindEvents" />
            </div>
        </form>
    </div>
</asp:Content>