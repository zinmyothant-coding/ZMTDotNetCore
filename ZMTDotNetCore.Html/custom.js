function successMessage(message) {
  Swal.fire({
    title: "Success",
    text: message,
    icon: "success"
  });
}
function errorMessage(message) {
  Swal.fire({
    title: "Error",
    text: message,
    icon: "Error"
  });
}

function confirm(id) {
  Swal.fire({
    title: "Confirm",
    text: "Are you sure want to delete?",
    icon: "warning",
    showCancelButton: true,
    confirmButtonColor: "#3085d6",
    cancelButtonColor: "#d33",
    confirmButtonText: "Yes"
  }).then((result) => {
    if (!result.isConfirmed) return;
    let lst = getBlogs();
    const data = lst.filter(s => s.id !== id);
    let jsonStr = JSON.stringify(data);
    localStorage.setItem(blog, jsonStr);
    Notiflix.Loading.dots();
    setTimeout(() => {
    Notiflix.Loading.remove();
    }, 1000);
    getBlogTable();
  });
}
function confirmMessage(id){
  let myPromise = new Promise(function(success, error) {
    Swal.fire({
      title: "Confirm",
      text: "Are you sure want to delete?",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Yes"
    }).then((result) => {
      if (!result.isConfirmed) return;
      success(); 
      let lst = getBlogs();
      const data = lst.filter(s => s.id !== id);
      let jsonStr = JSON.stringify(data);
      localStorage.setItem(blog, jsonStr); 
    }); 
    }); 
    myPromise.then(
      function(value) { /* code if successful */ 
        Notiflix.Loading.dots();
        setTimeout(() => {
          getBlogTable();
          Notiflix.Loading.remove();
          successMessage();
        }, 1000); 
      },
      function(error) { /* code if some error */ 
        errorMessage();
      }
    );
}