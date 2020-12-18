''
'' This program is free software; you can redistribute it and/or modify
'' it under the terms of the GNU General Public License as published by
'' the Free Software Foundation; either version 2 of the License, or
'' (at your option) any later version.
''
'' This program is distributed in the hope that it will be useful,
'' but WITHOUT ANY WARRANTY; without even the implied warranty of
'' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'' GNU General Public License for more details.
''
'' You should have received a copy of the GNU General Public License
'' along with this program; if not, write to the Free Software
'' Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
'' 

Imports Microsoft.VisualStudio.SourceSafe.Interop

''' <summary>
''' This class represents a VSS item.
''' </summary>
''' <remarks></remarks>
Public Class SSItem

    ''' <summary>
    ''' VSS item type (path or file)
    ''' </summary>
    ''' <remarks></remarks>
    Protected m_type As SSItemType

    ''' <summary>
    ''' Checked out flag.
    ''' </summary>
    ''' <remarks></remarks>
    Protected m_checkedOut As Boolean = False

    ''' <summary>
    ''' Item name.
    ''' </summary>
    ''' <remarks></remarks>
    Protected m_name As String

    ''' <summary>
    ''' Subitems under this item.
    ''' </summary>
    ''' <remarks></remarks>
    Protected m_subitems As List(Of SSItem)

    ''' <summary>
    ''' Actual VSSItem reference.
    ''' </summary>
    ''' <remarks></remarks>
    Protected m_vssitem As VSSItem

    ''' <summary>
    ''' Number of revisions for this item.
    ''' </summary>
    ''' <remarks></remarks>
    Protected m_versions As Integer

    ''' <summary>
    ''' Flag indicating whether VSS stores a single
    ''' version of this item.
    ''' </summary>
    ''' <remarks></remarks>
    Protected m_isSingleVersion As Boolean = False

    ''' <summary>
    ''' Returns the item type.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Type() As SSItemType
        Get
            Return m_type
        End Get
    End Property

    ''' <summary>
    ''' Returns a flag indicating whether this item is checked out.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CheckedOut() As Boolean
        Get
            Return m_checkedOut
        End Get
    End Property

    ''' <summary>
    ''' Returns the item name.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Name() As String
        Get
            Return m_name
        End Get
    End Property

    ''' <summary>
    ''' Get/set a list with items under this item.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SubItems() As List(Of SSItem)
        Get
            Return m_subitems
        End Get
        Set(ByVal value As List(Of SSItem))
            m_subitems = value
        End Set
    End Property

    ''' <summary>
    ''' Returns the actual VSSItem reference for this instance.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property VSS() As VSSItem
        Get
            Return m_vssitem
        End Get
    End Property

    ''' <summary>
    ''' Returns the number of revisions for this item.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Versions() As Integer
        Get
            Return m_versions
        End Get
    End Property

    ''' <summary>
    ''' Get/set flag indicating whether VSS stores a single version of this item.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsSingleVersionFile() As Boolean
        Get
            Return m_isSingleVersion
        End Get
        Set(ByVal value As Boolean)
            m_isSingleVersion = value
        End Set
    End Property

    ''' <summary>
    ''' Default constructor.
    ''' </summary>
    ''' <param name="vss">VSSItem instance.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal vss As VSSItem)
        m_name = vss.Name
        m_vssitem = vss
        m_subitems = New List(Of SSItem)
        If vss.Type = 0 Then
            m_type = SSItemType.Path
            m_versions = 0
        Else
            m_type = SSItemType.File
            m_checkedOut = (vss.IsCheckedOut <> VSSFileStatus.VSSFILE_NOTCHECKEDOUT)
            'The count of vss.Versions is the number of items in the VSS API structure
            'and not the real number of versions.
            'm_versions = vss.Versions.Count
            m_versions = vss.VersionNumber
        End If
    End Sub

    ''' <summary>
    ''' Adds a subitem.
    ''' </summary>
    ''' <param name="vss">VSSItem instance of subitem.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddSubItem(ByVal vss As VSSItem) As SSItem
        Dim o As New SSItem(vss)
        m_subitems.Add(o)
        Return o
    End Function

End Class

''' <summary>
''' Item type.
''' </summary>
''' <remarks></remarks>
Public Enum SSItemType
    ''' <summary>
    ''' Indicates that the item is a path.
    ''' </summary>
    ''' <remarks></remarks>
    Path = 0

    ''' <summary>
    ''' Indicates that the item is a file.
    ''' </summary>
    ''' <remarks></remarks>
    File = 1
End Enum