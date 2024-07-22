Public Class frmStudent
    'Khai bao bien truy xuat DB tu lop DBAcccess
    Private _DBAccess As New DataBaseAccess

    'Khai bao bien de biet trang thai dang la Edit hay Insert
    Private _isEdit As Boolean = False
    Public Sub New(IsEdit As Boolean)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _isEdit = IsEdit
    End Sub

    'Dinh nghia Ham them ban ghi Sinh vien vao database
    Private Function InsertStudent() As Boolean
        Dim sqlQuery As String = "INSERT INTO SinhVien ( MASV, TenSV, Lop, GioiTinh, DiaChi) "
        sqlQuery += String.Format("VALUES ('{0}','{1}','{2}','{3}','{4}')",
                                txtMaSV.Text, txtTSV.Text, txtLOP.Text, txtGT.Text, txtDC.Text)
        Return _DBAccess.ExecuteNoneQuery(sqlQuery)
    End Function

    'Dinh nghia Ham Update 
    Private Function UpdateStudent() As Boolean
        Dim sqlQuery As String = String.Format("UPDATE SinhVien SET MaSV ='{0}', TenSV = '{1}', GioiTinh ='{2}', Lop = '{3}', DiaChi ='{3}'",
                                            Me.txtMaSV.Text, Me.txtLOP.Text, Me.txtGT.Text, Me.txtDC.Text, Me.txtTSV.Text)
        Return _DBAccess.ExecuteNoneQuery(sqlQuery)
    End Function

    'Dinh nghia ham kiem tra gia tri truoc khi insert du lieu vao database
    Private Function IsEmpty() As Boolean
        Return (String.IsNullOrEmpty(txtMaSV.Text) OrElse String.IsNullOrEmpty(txtTSV.Text) OrElse
                String.IsNullOrEmpty(txtLOP.Text) OrElse String.IsNullOrEmpty(txtGT.Text) OrElse
                String.IsNullOrEmpty(txtDC.Text))
    End Function

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If IsEmpty() Then   'Kiem tra truong du lieu truoc khi thuc hien THEM, SUA
            MessageBox.Show("Hay nhap gia tri vao truoc khi ghi vao database", "Error", MessageBoxButtons.OK)
        Else
            If _isEdit Then     'Neu la Edit thi goi ham Update
                If UpdateStudent() Then 'Neu Update thanh cong thi thong bao
                    MessageBox.Show("Sua du lieu thanh cong!", "Infomation", MessageBoxButtons.OK)
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                Else        'Neu co loi khi sua thi thong bao loi
                    MessageBox.Show("Loi sua du lieu", "Error", MessageBoxButtons.OK)
                    Me.DialogResult = Windows.Forms.DialogResult.No
                End If
            Else                'Neu khong phai Edit thi goi ham Insert
                If InsertStudent() Then 'Neu insert thanh cong thi thong bao
                    MessageBox.Show("Them du lieu thanh cong!", "Infomation", MessageBoxButtons.OK)
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                Else
                    MessageBox.Show("Loi Them du lieu!", "Error", MessageBoxButtons.OK)
                    Me.DialogResult = Windows.Forms.DialogResult.No
                End If
            End If

            Me.Close()
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmStudent_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub txtMSV_TextChanged(sender As Object, e As EventArgs) Handles txtMaSV.TextChanged

    End Sub

    Private Sub txtTSV_TextChanged(sender As Object, e As EventArgs) Handles txtTSV.TextChanged

    End Sub

    Private Sub txtMaSV_TextChanged(sender As Object, e As EventArgs) Handles txtMaSV.TextChanged

    End Sub
End Class