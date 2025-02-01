let currentIndex = 0;
     
             function showSlide(index) {
                 const carousel = document.getElementById('carousel');
                 const items = document.querySelectorAll('.carousel-item');
                 const totalItems = items.length;
     
                 if (index >= totalItems) {
                     currentIndex = 0;
                 } else if (index < 0) {
                     currentIndex = totalItems - 1;
                 } else {
                     currentIndex = index;
                 }
     
                 const offset = -currentIndex * (items[0].offsetWidth - 160);
                 carousel.style.transform = `translateX(${offset}px)`;
             }
     
             function nextSlide() {
                 showSlide(currentIndex + 1);
             }
     
             function prevSlide() {
                 showSlide(currentIndex - 1);
             }
             
             setInterval(() => {
                 nextSlide();
             }, 3000);