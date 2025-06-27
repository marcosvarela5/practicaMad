<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master"  AutoEventWireup="true" CodeBehind="ShowAll.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Recommendations.ShowAll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <br />
    <form runat="server">
        <p><asp:Label ID="lblNoRecomendations" runat="server" meta:resourcekey="lblNoRecomendations"></asp:Label></p>
        <asp:GridView ID="recommendationGrid" runat="server" CssClass="Grid" GridLines="None"
            AutoGenerateColumns="False" HorizontalAlign="Center" ShowHeaderWhenEmpty="True" OnRowDataBound="GridView_DataBound">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" visible="false"/>
                <asp:BoundField DataField="date" HeaderText="<%$ Resources: date %>" meta:resourcekey="BoundField9" />
                <asp:BoundField DataField="text" HeaderText="<%$ Resources: description %>" meta:resourcekey="BoundField10" />
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEvent" runat="server" CommandArgument='<%# Eval("EventId") %>' CommandName= "comentarios" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </form>
    <br />
    
    <div class="previousNextLinks">
    <span class="previousLink">
        <asp:HyperLink ID="lnkPrevious" runat="server"
            Visible="False" meta:resourcekey="lnkPreviousResource1"></asp:HyperLink>
    </span><span class="nextLink">
        <asp:HyperLink ID="lnkNext" runat="server" Visible="False" meta:resourcekey="lnkNextResource1"></asp:HyperLink>
    </span>
</div>
    <br />
</asp:Content>