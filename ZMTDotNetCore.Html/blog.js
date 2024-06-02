const blog = "blog";
let blogid = null;
//DeleteBlog("6a0da886-3ebf-446b-87b2-362be2774772");
//UpdateBlog("79bfac50-4543-4717-9db0-875ba9320919","test");
//createBlog();
//readBlog();
getBlogTable();
function readBlog() {
    localStorage.getItem(blog);
}
function getBlogs() {
    let item = localStorage.getItem(blog);
    let lst = [0];
    if (item !== null) {
        lst = JSON.parse(item);
    }
    return lst;
}
function createBlog(title, author, content) {
    let lst = getBlogs();
    const requestModel = {
        id: uuidv4(),
        title: title,
        author: author,
        content: content
    };
    lst.push(requestModel);
    console.log(lst);
    let jsonStr = JSON.stringify(lst);
    localStorage.setItem(blog, jsonStr);
}
function EditBlog(id) {
    let lst = getBlogs();
    const item = lst.filter(s => s.id === id);
    if (item.lenght == 0) {
        ErrorMessageBox('No data founc');
        return;
    }
    const data = item[0];
    blogid = data.id;
    $('#txtTitle').val(data.title);
    $('#txtAuthor').val(data.author);
    $('#txtContent').val(data.content);
}
function UpdateBlog(id, title, author, content) {
    let lst = getBlogs();
    const item = lst.filter(s => s.id === id);
    if (item.lenght == 0) {
        ErrorMessageBox('No data founc');
        return;
    }
    const data = item[0];
    data.id = id;
    data.content = content;
    data.author = author;
    data.title = title;

    const index = lst.findIndex(s => s.id === id);
    lst[index] = data;
    const jsonStr = JSON.stringify(lst);
    localStorage.setItem(blog, jsonStr);
    getBlogTable();
}
function DeleteBlog(id) {
    let lst = getBlogs();
    const data = lst.filter(s => s.id !== id);
    let jsonStr = JSON.stringify(data);
    localStorage.setItem(blog, jsonStr);
    getBlogTable();
}
function uuidv4() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'
        .replace(/[xy]/g, function (c) {
            const r = Math.random() * 16 | 0,
                v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
}
$('#btnSave').click(function () {
    let title = $('#txtTitle').val();
    let author = $('#txtAuthor').val();
    let content = $('#txtContent').val();
    if (blogid == null) {
        createBlog(title, author, content);
    } else {
        UpdateBlog(blogid, title, author, content);
        blogid = null;
    }
    SuccessMessageBox('Save Successfully');
    $('#txtTitle').val('');
    $('#txtAuthor').val('');
    $('#txtContent').val('');
    $('#txtTitle').focus();
    getBlogTable();
})
function getBlogTable() {
    let lst = getBlogs();
    let htmlRows = '';
    let count = 0;
    lst.forEach(item => {
        const htmlRow = `
        <tr>
        <td scope="col"> <button type="button" class="btn btn-warning" id="${item.id}" onclick="EditBlog('${item.id}')">Edit</button> <button type="button" class="btn btn-danger" id="${item.id}" onclick="DeleteBlog('${item.id}' )">Delete</button></td>
        <td scope="col">${++count}</td>
        <td scope="col">${item.title}</td>
        <td scope="col">${item.content}</th>
        <td scope="col">${item.author}</td>
        </tr>
        `;
        htmlRows += htmlRow;
    });
    $('#tblbody').html(htmlRows);
}
function SuccessMessageBox(message) {
    alert(message);
}
function ErrorMessageBox(message) {
    alert(message);
}