const blog = "blog";
DeleteBlog("6a0da886-3ebf-446b-87b2-362be2774772");
//UpdateBlog("79bfac50-4543-4717-9db0-875ba9320919","test");
//createBlog();
readBlog();
function readBlog() {
    localStorage.getItem(blog);
}
function createBlog() {
    let item = localStorage.getItem(blog);
    let lst = [];
    if (item !== "null") {
        lst = JSON.parse(item);
    }

    const requestModel = {
        id: uuidv4(),
        content: "testcontent"
    };
    lst.push(requestModel);
    console.log(lst);
    let jsonStr = JSON.stringify(lst);
    localStorage.setItem(blog, jsonStr);
}
function UpdateBlog(id, content) {
    const items = localStorage.getItem(blog);
    let lst = [];
    if (items !== "null") {
        lst = JSON.parse(items);
    }
    const item = lst.filter(s => s.id === id);
    if (item.lenght == 0) {
        console.log("No data found.");
        return;
    }
    const data = item[0];
    data.id = id;
    data.content = content;

    const index = lst.findIndex(s => s.id === id);
    lst[index] = data;
    const jsonStr = JSON.stringify(lst);
    localStorage.setItem(blog, jsonStr);
}
function DeleteBlog(id) {
    const items = localStorage.getItem(blog);
    let lst = [];
    if (items !== "null") {
        lst = JSON.parse(items);
    }
    const data = lst.filter(s => s.id !== id);
    let jsonStr = JSON.stringify(data);
    localStorage.setItem(blog,jsonStr);
}
function uuidv4() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'
        .replace(/[xy]/g, function (c) {
            const r = Math.random() * 16 | 0,
                v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
}