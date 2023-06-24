using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductCatalog.BL.Consts;
using ProductCatalog.BL.DTOs;
using ProductCatalog.BL.UnitOfWork;
using ProductCatalog.DAL.Models;
using ProductCatalog.PL.Helpers;

namespace ProductCatalog.PL.Controllers
{
    [Authorize(Roles = $"{Privilege.Admin},{Privilege.User}")]
    public class ProductController : Controller
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        #endregion

        #region Constructor
        public ProductController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #endregion

        #region Actions
        public async Task<IActionResult> Index()
        {
            ApplicationUser? user = await _userManager.GetUserAsync(User);

            if (user is not null)
            {
                bool isAdmin = await _userManager.IsInRoleAsync(user, Privilege.Admin);
                if (isAdmin)
                {
                    IEnumerable<ProductDto> products = _mapper.Map<IEnumerable<ProductDto>>(await _unitOfWork.Products.GetAllAsync());
                    return View(products);
                }
                else
                {
                    IEnumerable<ProductDto> products = _mapper.Map<IEnumerable<ProductDto>>
                        (await _unitOfWork.Products.FindByConditionAsync(p =>
                            DateTime.Compare(p.StartDate, DateTime.Now.Date) <= 0 &&
                            DateTime.Compare(p.StartDate.AddDays(7 * p.Duration).Date, DateTime.Now.Date) >= 0
                        ));
                    return View(products);
                }
            }

            return View();
        }

        public async Task<IActionResult> Preview(int id)
        {
            ProductDto product = _mapper.Map<ProductDto>(await _unitOfWork.Products.GetByIdAsync(id));
            if (product is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            return View(product);
        }

        [Authorize(Roles = Privilege.Admin)]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _unitOfWork.Categories.GetAllAsync(), "Id", "Name");
            return View();
        }

        [Authorize(Roles = Privilege.Admin)]
        [HttpPost]
        public async Task<IActionResult> Create(ProductDto product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    product.CreatedOn = DateTime.Now;
                    product.UpdatedAt = DateTime.Now;
                    var user = await _userManager.FindByNameAsync(User.Identity.Name);
                    product.CreatedBy = new Guid(user.Id); // User Id

                    if (DateTime.Compare(product.StartDate, DateTime.Now.Date) < 0)
                    {
                        ViewBag.EarlierStartDate = $"Start Date field must be later than or equals to `{DateTime.Now.ToShortDateString()}`";
                        ViewBag.Categories = new SelectList(await _unitOfWork.Categories.GetAllAsync(), "Id", "Name");
                        return View();
                    }

                    var category = await _unitOfWork.Categories.GetByIdAsync(product.CategoryId);
                    if (category is null)
                    {
                        ViewBag.InvalidCategory = $"Invalid Category Choosen";
                        ViewBag.Categories = new SelectList(await _unitOfWork.Categories.GetAllAsync(), "Id", "Name");
                        return View();
                    }

                    _unitOfWork.Products.Add(_mapper.Map<Product>(product));
                    await _unitOfWork.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                // Log in a file
                LogError.LogErrorInAFile(ex, "Create Product");
            }

            ViewBag.Categories = new SelectList(await _unitOfWork.Categories.GetAllAsync(), "Id", "Name");
            return View();
        }

        [Authorize(Roles = Privilege.Admin)]
        public async Task<IActionResult> Update(int id)
        {
            ProductDto product = _mapper.Map<ProductDto>(await _unitOfWork.Products.GetByIdAsync(id));
            if (product is null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            ViewBag.Categories = new SelectList(await _unitOfWork.Categories.GetAllAsync(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [Authorize(Roles = Privilege.Admin)]
        [HttpPost]
        public async Task<IActionResult> Update(ProductDto product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    product.UpdatedAt = DateTime.Now;
                    //product.CreatedBy = 1; // User Id

                    if (DateTime.Compare(product.StartDate, DateTime.Now.Date) < 0)
                    {
                        ViewBag.EarlierStartDate = $"Start Date field must be later than or equals to `{DateTime.Now.ToShortDateString()}`";
                        ViewBag.Categories = new SelectList(await _unitOfWork.Categories.GetAllAsync(), "Id", "Name", product.CategoryId);
                        return View(product);
                    }

                    var category = await _unitOfWork.Categories.GetByIdAsync(product.CategoryId);
                    if (category is null)
                    {
                        ViewBag.InvalidCategory = $"Invalid Category Choosen";
                        ViewBag.Categories = new SelectList(await _unitOfWork.Categories.GetAllAsync(), "Id", "Name", product.CategoryId);
                        return View(product);
                    }

                    var updatedProduct = _unitOfWork.Products.Update(_mapper.Map<Product>(product));
                    await _unitOfWork.SaveChangesAsync();
                    ViewBag.Categories = new SelectList(await _unitOfWork.Categories.GetAllAsync(), "Id", "Name", updatedProduct.CategoryId);
                    return View(_mapper.Map<ProductDto>(updatedProduct));
                }
            }
            catch (Exception ex)
            {
                // Log in a file
                LogError.LogErrorInAFile(ex, "Update Product");
            }

            ViewBag.Categories = new SelectList(await _unitOfWork.Categories.GetAllAsync(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [Authorize(Roles = Privilege.Admin)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Product product = await _unitOfWork.Products.GetByIdAsync(id);
                if (product is null)
                {
                    return NotFound();
                }
                _unitOfWork.Products.Delete(product);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log in a file
                LogError.LogErrorInAFile(ex, "Delete Product");
                return NotFound();
            }
            return Ok();
        }
        #endregion
    }
}
