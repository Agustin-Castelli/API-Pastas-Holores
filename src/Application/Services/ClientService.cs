using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IBaseRepository<Client> _clientRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public ClientService(IBaseRepository<Client> clientRepository, ICartRepository cartRepository, IProductRepository productRepository)
        {
            _clientRepository = clientRepository;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public Client Create(ClientCreateRequest clientCreateRequest)
        {
            var newObj = new Client();
            newObj.Username = clientCreateRequest.Username;
            newObj.Password = clientCreateRequest.Password;
            newObj.Email = clientCreateRequest.Email;
            newObj.FirstName = clientCreateRequest.FirstName;
            newObj.LastName = clientCreateRequest.LastName;
            newObj.Adress = clientCreateRequest.Adress;

            _cartRepository.CreateCartForClient(newObj.Id);

            return _clientRepository.Add(newObj);
        }

        public void Update(int id, ClientUpdateRequest clientUpdateRequest) 
        {
            var obj = _clientRepository.GetById(id);

            if (obj == null)
            {
                throw new NotFoundException(nameof(Client), id);
            }

            if (clientUpdateRequest.Username != string.Empty) obj.Username = clientUpdateRequest.Username;
            if (clientUpdateRequest.Password != string.Empty) obj.Password = clientUpdateRequest.Password;
            if (clientUpdateRequest.Email != string.Empty) obj.Email = clientUpdateRequest.Email;
            if (clientUpdateRequest.FirstName != string.Empty) obj.FirstName = clientUpdateRequest.FirstName;
            if (clientUpdateRequest.LastName != string.Empty) obj.LastName = clientUpdateRequest.LastName;
            if (clientUpdateRequest.Adress != string.Empty) obj.Adress = clientUpdateRequest.Adress;

            _clientRepository.Update(obj);
        }

        public void Delete(int id)
        {
            var obj = _clientRepository.GetById(id);

            if (obj == null)
            {
                throw new NotFoundException(nameof(Client), id);
            }

            _clientRepository.Delete(obj);
        }

        public List<ClientDto> GetAll() 
        { 
            var list = _clientRepository.GetAll();

            return ClientDto.CreateList(list);
        }

        public List<Client> GetAllFullData()
        {
            return _clientRepository.GetAll();
        }

        public ClientDto GetById(int id) 
        {
            var obj = _clientRepository.GetById(id);

            var dto = ClientDto.Create(obj);

            return dto;
        }






              // ↓ ↓ ↓ ↓  METODOS ESPECÍFICOS ↓ ↓ ↓ ↓ 






        public CartDto? GetCart(int clientId)
        {
            if (_clientRepository.GetById(clientId).Id == clientId)
            {
                var cart = _cartRepository.GetCart(clientId);

                var dto = CartDto.Create(cart);


                return dto;
            }

            throw new NotFoundException(nameof(Client), clientId);
        }

        public void AddCartProducts(int clientId, string productName) 
        {
            if (_clientRepository.GetById(clientId).Id == clientId)    // Busco el cliente
            {

                var productFound = _productRepository.GetByName(productName);
                var productInCart = _cartRepository.GetCart(clientId).Products.FirstOrDefault(productFound);

                if (productFound != null)    // Busco el producto en la lista de productos
                {
                    if (productInCart != null)   // Busco el producto en el carrito
                    {
                        productInCart.Units += 1;
                        productInCart.Price += productInCart.Price;
                    }
                    else
                    {
                        _cartRepository.GetCart(clientId).Products.Add(productFound);
                    }

                    _cartRepository.CalculateTotalProductPrice(clientId);
                    _cartRepository.Update();
                }
                else
                {
                    throw new NotFoundException(nameof(Client), clientId);
                }
            }
        }

        public void DeleteCartProducts(int clientId, string productName) // Busco un cliente, si lo encuentra, busco el producto por el nombre, si lo encuentra, lo saco de la lista de productos del carrito.
        {
            if (_clientRepository.GetById(clientId).Id == clientId)
            {

                var productFound = _productRepository.GetByName(productName);

                if (productFound != null ) 
                {

                    _cartRepository.GetCart(clientId).Products.Remove(productFound);
                    _cartRepository.CalculateTotalProductPrice(clientId);
                    _cartRepository.Update();
                }
            }
            else
            {
                throw new NotFoundException(nameof(Client), clientId);
            }
        }

        public void CompletePurchase(int clientId, string paymentMethod)
        {
            if (_clientRepository.GetById(clientId).Id == clientId)
            {
                _cartRepository.GetCart(clientId).PaymentMethod = paymentMethod;
                _cartRepository.GetCart(clientId).Status = Domain.Enums.CartStatus.Completed;
                _cartRepository.Update();
            }
            else
            {
                throw new NotFoundException(nameof(Client), clientId);
            }
        }
    }
}