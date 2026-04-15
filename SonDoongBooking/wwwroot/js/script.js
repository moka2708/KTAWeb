document.addEventListener('DOMContentLoaded', () => {
    
    // 1. Navbar Scroll
    window.addEventListener('scroll', () => {
        const nav = document.querySelector('.navbar');
        window.scrollY > 50 ? nav.classList.add('scrolled') : nav.classList.remove('scrolled');
    });

    // 2. Scroll Reveal & Number Counter
    let counted = false; // Biến cờ để chỉ đếm số 1 lần

    const reveal = () => {
        const reveals = document.querySelectorAll('.reveal');
        const windowHeight = window.innerHeight;

        reveals.forEach(el => {
            const elementTop = el.getBoundingClientRect().top;
            if (elementTop < windowHeight - 100) {
                el.classList.add('active');

                // Kích hoạt bộ đếm số khi cuộn tới phần Banner
                if (el.querySelector('#counter-section') && !counted) {
                    startCounters();
                    counted = true;
                }
            }
        });
    };
    
    window.addEventListener('scroll', reveal);
    reveal();

    // 3. Hàm chạy đếm số (Counter Animation)
    const startCounters = () => {
        const counters = document.querySelectorAll('.counter');
        const speed = 100; // Tốc độ đếm (càng nhỏ càng nhanh)

        counters.forEach(counter => {
            const updateCount = () => {
                const target = +counter.getAttribute('data-target');
                const count = +counter.innerText;
                const inc = target / speed;

                if (count < target) {
                    counter.innerText = Math.ceil(count + inc);
                    setTimeout(updateCount, 20);
                } else {
                    counter.innerText = target;
                }
            };
            updateCount();
        });
    };

    // 4. Back to Top Button
    const btt = document.getElementById('backToTop');
    window.addEventListener('scroll', () => {
        window.scrollY > 400 ? btt.classList.add('show') : btt.classList.remove('show');
    });
    btt.addEventListener('click', () => window.scrollTo({top: 0, behavior: 'smooth'}));
});

// Modal Ảnh
window.openModal = (s) => {
    document.getElementById('modalImg').src = s;
    new bootstrap.Modal(document.getElementById('imageModal')).show();
};