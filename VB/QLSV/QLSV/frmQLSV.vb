Imports Microsoft.VisualBasic.Logging

Public Class frmQLSV
    'Khai bao bien de truy xuat DB tu lop DataBaseAccess
    Private _DBAccess As New DataBaseAccess

    'Khai bao bien trang thai kiem tra du lieu dang Load
    Private _isLoading As Boolean = False

    'Dinh nghia thu tuc load du lieu tu bang Lop vao ComBobox
    Private Sub LoadDataOnComBobox()
        Dim sqlQuery As String = "SELECT MaSV, TenSV FROM dbo.SinhVien"
        Dim dTable As DataTable = _DBAccess.GetDataTable(sqlQuery)
        Me.cmbClass.DataSource = dTable
        Me.cmbClass.ValueMember = "MaSV"
        Me.cmbClass.DisplayMember = "TenSV"
    End Sub

    'Dinh nghia thu tuc load du lieu tu bang Sinh vien theo tung Lop vao Gridview
    Private Sub LoadDataOnGridView(MaSV As String)
        Dim sqlQuery As String =
            String.Format("SELECT MaSV, TenSV, Lop,  GioiTinh, Diem, DiaChi FROM dbo.SinhVien ", MaSV)

        Dim dTable As DataTable = _DBAccess.GetDataTable(sqlQuery)
        Me.dgvStudents.DataSource = dTable
        With Me.dgvStudents
            .Columns(0).HeaderText = "MaSV"
            .Columns(1).HeaderText = "TenSV"
            .Columns(2).HeaderText = "Lop"
            .Columns(3).HeaderText = "GioiTinh"
            .Columns(4).HeaderText = "Diem"
            .Columns(5).HeaderText = "DiaChi"
            .Columns(5).Width = 200
        End With
    End Sub

    Private Sub frmQLSV_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        _isLoading = True   'True khi du lieu bat dau load

        LoadDataOnComBobox()
        LoadDataOnGridView(Me.cmbClass.SelectedValue)

        _isLoading = False  'False khi load du lieu xong
    End Sub




    'Dinh nghia thu tuc hien thi ket qua Search: theo phuong phap tuong tu - Tim kiem tuong tu
    Private Sub SearchStudent(MaSV As String, value As String)
        Dim sqlQuery As String =
        String.Format("SELECT MaSV, TenSV, Lop, GioiTinh, Diem, DiaChi FROM dbo.SinhVien ", MaSV)
        If Me.cmbSearch.SelectedIndex = 0 Then      'Tim theo Ma Sinh Vien
            sqlQuery += String.Format(" WHERE MaSV LIKE '{0}%'", value)
        ElseIf Me.cmbSearch.SelectedIndex = 1 Then  'Tim theo Ten Sinh Vien
            sqlQuery += String.Format(" WHERE TenSV LIKE '{0}%'", value)
        End If
        Dim dTable As DataTable = _DBAccess.GetDataTable(sqlQuery)
        Me.dgvStudents.DataSource = dTable
        With Me.dgvStudents

            .Columns(0).HeaderText = "MaSV"
            .Columns(1).HeaderText = "TenSV"
            .Columns(2).HeaderText = "Lop"
            .Columns(3).HeaderText = "GioiTinh"
            .Columns(4).HeaderText = "Diem"
            .Columns(5).HeaderText = "DiaChi"
            .Columns(5).Width = 200
        End With
    End Sub
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        SearchStudent(Me.cmbClass.SelectedValue, Me.txtSearch.Text)
    End Sub


    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim frm As New frmStudent(False)
        frm.txtMaSV.Text = Me.cmbClass.SelectedValue
        frm.ShowDialog()
        If frm.DialogResult = Windows.Forms.DialogResult.OK Then
            'Load du lieu
            LoadDataOnGridView(Me.cmbClass.SelectedValue)
        End If
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim frm As New frmStudent(True)
        frm.txtMaSV.Text = Me.cmbClass.SelectedValue
        frm.txtTSV.ReadOnly = True  'Chi cho doc, truong nay khong cho phep thay doi khi sua du lieu
        With Me.dgvStudents
            frm.txtTSV.Text = .Rows(.CurrentCell.RowIndex).Cells("MaSV").Value
            frm.txtTSV.Text = .Rows(.CurrentCell.RowIndex).Cells("TenSV").Value
            frm.txtLOP.Text = .Rows(.CurrentCell.RowIndex).Cells("Lop").Value
            frm.txtGT.Text = .Rows(.CurrentCell.RowIndex).Cells("GioiTinh").Value
            frm.txtDiem.Text = .Rows(.CurrentCell.RowIndex).Cells("Diem").Value
            frm.txtDC.Text = .Rows(.CurrentCell.RowIndex).Cells("DiaChi").Value
        End With
        frm.ShowDialog()
        If frm.DialogResult = Windows.Forms.DialogResult.OK Then 'Sua du lieu thanh cong thi load lai du lieu vao gridview
            LoadDataOnGridView(Me.cmbClass.SelectedValue)
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        'Khai bao bien lay StudentID ma dong can xoa da duoc chon tren gridview
        Dim MaSV As String = Me.dgvStudents.Rows(Me.dgvStudents.CurrentCell.RowIndex).Cells("MaSV").Value

        'Khai bao cau lenh Query de xoa
        Dim sqlQuery As String = String.Format("DELETE SinhVien WHERE MaSV = '{0}'", MaSV)

        'Thuc hien xoa
        If _DBAccess.ExecuteNoneQuery(sqlQuery) Then    'Xoa thanh cong thi thong bao
            MessageBox.Show("Da xoa du lieu thanh cong!")
            'Load lai du lieu tren Gridview
            LoadDataOnGridView(Me.cmbClass.SelectedValue)
        Else
            MessageBox.Show("Loi xoa du lieu!")
        End If

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub

    Private Sub cmbSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSearch.SelectedIndexChanged

    End Sub
End Class
