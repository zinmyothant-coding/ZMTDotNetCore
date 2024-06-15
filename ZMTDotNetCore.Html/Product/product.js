const products = [
    {
      id: 1,
      name: "Product 1",
      description: "Description..",
      price: 500.05,
      quantity: 1,
    },
    {
      id: 2,
      name: "Product 2",
      description: "Description..",
      price: 8000.5,
      quantity: 0,
    },
    {
      id: 3,
      name: "Product 3",
      description: "Description..",
      price: 1000.5,
      quantity: 0,
    },
  ];
  
  const cart = [];
  
  const productList = document.getElementById("product-list");
  const cartList = document.getElementById("cart-list");
  const cartTotal = document.getElementById("cart-total");
  var items=document.getElementById("cart-item");
  
  function renderProducts() {
    productList.innerHTML = "";
    products.forEach((product) => {
      const productItem = document.createElement("div");
      productItem.classList.add("product-item");
      productItem.innerHTML = `
        <h4>${product.name}</h4>
        <p>${product.description}</p>
        <p>${product.price.toFixed(2)}</p>
        <button data-product-id="${product.id}" class="btn btn-success">Add To Cart</button>
      `;
      productList.appendChild(productItem);
    });
    setupAddToCartButtons();
  }
  
  function renderCart() {
    cartList.innerHTML = "";
    let total = 0;
    let item=0;
    cart.forEach((product) => {
      const cartItem = document.createElement("div");
      cartItem.classList.add("cart-item");
      cartItem.innerHTML = `
        <p>Name &nbsp;&nbsp;&nbsp;${product.name}</p>
        <p>Quentity&nbsp;&nbsp;${product.quantity}</p>
        <p>Price &nbsp;&nbsp;&nbsp;&nbsp;${product.price.toFixed(2)}</p> 
      `;
      cartList.appendChild(cartItem);
      total += product.price * product.quantity;
      item+=product.quantity;
    });
    cartTotal.textContent = `${total.toFixed(2)}`;
    document.getElementById("cart-item").innerHTML=item;
    $('#card-item').val(item); 
    setupRemoveFromCartButtons();
  }
  
  function addToCart(productId) {
    const product = products.find((p) => p.id === productId);
    const existingItem = cart.find((item) => item.id === productId);
    if (existingItem) {
      existingItem.quantity++;
    } else {
      cart.push({ ...product, quantity: 1 });
    }
    renderCart();
    saveCartToLocalStorage();
  }
  
  function removeFromCart(productId) {
    const index = cart.findIndex((item) => item.id === productId);
    if (index !== -1) {
      cart.splice(index, 1);
    }
    renderCart();
    saveCartToLocalStorage();
  }
  
  function setupAddToCartButtons() {
    const addToCartButtons = document.querySelectorAll(".product-item button");
    addToCartButtons.forEach((button) => {
      button.addEventListener("click", (event) => {
        const productId = parseInt(event.target.dataset.productId);
        addToCart(productId);
      });
    });
  }
  
  function setupRemoveFromCartButtons() {
    const removeButtons = document.querySelectorAll(".cart-item .remove-item");
    removeButtons.forEach((button) => {
      button.addEventListener("click", (event) => {
        const productId = parseInt(event.target.dataset.productId);
        removeFromCart(productId);
      });
    });
  }
  
  function saveCartToLocalStorage() {
    localStorage.setItem("cart", JSON.stringify(cart));
  }
  
  function loadCartFromLocalStorage() {
    const storedCart = localStorage.getItem("cart");
    if (storedCart) {
      cart.push(...JSON.parse(storedCart));
      renderCart();
    }
  }
  
  function emptyCart() {
    localStorage.removeItem("cart");
    cart.length = 0;
    renderCart();
  }
  
  renderProducts();
  renderCart();
  loadCartFromLocalStorage();

  function successMessage(message) {
    Swal.fire({
      title: "Success",
      text: message,
      icon: "success"
    });
  }

  function Ordercancel(isCancel){ 
    let text = (isCancel===true) ? 'Are you sure want to Cancel?' : 'Send Order!!';
    let title = (isCancel===true) ? 'Order cancel' : 'Order confirm!!';
    let icon= (isCancel===true) ? 'warning' : 'success';
    let myPromise = new Promise(function(success, error) {
      Swal.fire({
        title: title,
        text: text,
        icon: icon,
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes"
      }).then((result) => {
        if (!result.isConfirmed) return;
        success();  
      }); 
      }); 
      myPromise.then(
        function(value) { /* code if successful */ 
          Notiflix.Loading.dots();
          setTimeout(() => { 
            emptyCart();
            Notiflix.Loading.remove();
            successMessage();
          }, 1000); 
        },
        function(error) { /* code if some error */ 
          errorMessage();
        }
      );
  }
 // document.getElementById("empty-cart").addEventListener("click", Ordercancel(true));
