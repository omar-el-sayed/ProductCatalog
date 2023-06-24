function getCurrentURL() {
    return window.location.href;
}

if (getCurrentURL().includes('Product')) {
    document.getElementById('products').classList.add('active');
    document.getElementById('homeLi').classList.remove('menu-open');
    document.getElementById('homeLink').classList.remove('active');
    document.getElementById('dashboard').classList.remove('active');
}
