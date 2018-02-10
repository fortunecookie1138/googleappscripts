(function(){
	var app = angular.module('store', ['store-products']);
	
	app.controller('ReviewController', function(){
		this.review = {};
		
		this.addReview = function(product){
			product.reviews.push(this.review);
			this.review = {};
		};
	});
	
	app.controller('StoreController', function(){
		this.products = gems;
	});
	
	var gems = [
		{
			name: 'Dodecahedron',
			price: 2,
			description: '. . . It\'s shiny!',
			canPurchase: true,
			soldOut: false,
			images: [{full: 'images/image1.png'}],
			reviews: 
			[
				{
					stars: 5,
					body: "Dis is the best",
					author: "yaboi@holla.com"
				},
				{
					stars: 1,
					body: "poop",
					author: "grumps@nope.com"
				}
			]
		},
		{
			name: 'Pentagonal Gem',
			price: 5.95,
			description: '. . . It\'s a pentagon!',
			canPurchase: false,
			soldOut: false,
			images: [{full: 'images/image2.png'}],
			reviews: []
		},
		{
			name: 'Go Gem',
			price: 11.38,
			description: 'Roger Roger',
			canPurchase: true,
			soldOut: false,
			images: [{full: 'images/image3.png'}],
			reviews: 
			[
				{
					stars: 4,
					body: "a quality product, but dusty",
					author: "nono@istay.iclean"
				}
			]
		}
		]
})();