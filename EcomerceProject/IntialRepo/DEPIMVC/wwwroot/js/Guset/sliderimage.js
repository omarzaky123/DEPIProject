 // Hero Image Slider Functionality
 document.addEventListener("DOMContentLoaded", function () {
   const slides = document.querySelectorAll(".slide");
   const indicators = document.querySelectorAll(".indicator");
   const prevArrow = document.querySelector(".prev-arrow");
   const nextArrow = document.querySelector(".next-arrow");

   let currentSlide = 0;
   let slideInterval;

   // Initialize autoplay
   startSlideshow();

   // Set up event listeners
   prevArrow.addEventListener("click", () => {
     goToSlide(currentSlide - 1);
     resetTimer();
   });

   nextArrow.addEventListener("click", () => {
     goToSlide(currentSlide + 1);
     resetTimer();
   });

   indicators.forEach((indicator) => {
     indicator.addEventListener("click", () => {
       const slideIndex = parseInt(indicator.getAttribute("data-index"));
       goToSlide(slideIndex);
       resetTimer();
     });
   });

   // Pause autoplay when hovering over slider
   const heroSlider = document.querySelector(".hero-slider");
   heroSlider.addEventListener("mouseenter", () => {
     clearInterval(slideInterval);
   });

   heroSlider.addEventListener("mouseleave", () => {
     startSlideshow();
   });

   // Functions
   function goToSlide(index) {
     // Handle wrapping around beginning/end
     if (index < 0) {
       index = slides.length - 1;
     } else if (index >= slides.length) {
       index = 0;
     }

     // Remove active class from current slide and indicator
     slides[currentSlide].classList.remove("active");
     indicators[currentSlide].classList.remove("active");

     // Update current slide index
     currentSlide = index;

     // Add active class to new slide and indicator
     slides[currentSlide].classList.add("active");
     indicators[currentSlide].classList.add("active");
   }

   function startSlideshow() {
     slideInterval = setInterval(() => {
       goToSlide(currentSlide + 1);
     }, 5000); // Change slide every 5 seconds
   }

   function resetTimer() {
     clearInterval(slideInterval);
     startSlideshow();
   }

   // Add touch swipe functionality for mobile
   let touchStartX = 0;
   let touchEndX = 0;

   heroSlider.addEventListener(
     "touchstart",
     (e) => {
       touchStartX = e.changedTouches[0].screenX;
     },
     { passive: true }
   );

   heroSlider.addEventListener(
     "touchend",
     (e) => {
       touchEndX = e.changedTouches[0].screenX;
       handleSwipe();
     },
     { passive: true }
   );

   function handleSwipe() {
     const swipeThreshold = 50;

     if (touchEndX < touchStartX - swipeThreshold) {
       // Swipe left, go to next slide
       goToSlide(currentSlide + 1);
       resetTimer();
     }

     if (touchEndX > touchStartX + swipeThreshold) {
       // Swipe right, go to previous slide
       goToSlide(currentSlide - 1);
       resetTimer();
     }
   }
 });
