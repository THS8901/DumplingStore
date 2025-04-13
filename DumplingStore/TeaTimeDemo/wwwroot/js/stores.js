// p.7-54新增部分 // p.8-46修改部分
var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/admin/store/getall'
        },
        "columns": [
            { data: 'name', "width": "10%" },
            { data: 'address', "width": "30%" },
            { data: 'city', "width": "15%" },
            { data: 'phoneNumber', "width": "10%" },
            { data: 'description', "width": "35%" }
            //{ data: 'description', "width": "20%" },
            //{
            //    data: 'id',
            //    "render": function (data) {
            //        return `<div class="w-75 btn-group" role="group"> 
            //        <a href="/admin/store/upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
            //        <!--p.7-60新增部分-->
            //        <a onClick=Delete('/admin/store/delete/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
            //        </div>`
            //    },
            //    "width": "15%"
            //}
        ]
    });
}

//// p.7-60新增部分
//function Delete(url) {
//    Swal.fire({
//        title: 'Are you sure?',
//        text: "You won't be able to revert this!",
//        icon: 'warning',
//        showCancelButton: true,
//        confirmButtonColor: '#3085d6',
//        cancelButtonColor: '#d33',
//        confirmButtonText: 'Yes, delete it!'
//    }).then((result) => {
//        if (result.isConfirmed) {
//            $.ajax({
//                url: url,
//                type: 'DELETE',
//                success: function (data) {
//                    dataTable.ajax.reload();
//                    toastr.success(data.message);
//                }
//            })
//        }
//    })
//}