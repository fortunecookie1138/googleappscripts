(function(){
	var app = angular.module('store-products', [ ]);
	
	app.directive('productPanels', function(){
		return {
			restrict: 'E',
			templateUrl: 'product-panels.html',
			controller: function(){
				this.tab = 1;
				
				this.selectTab = function(setTab){
					this.tab = setTab;
				};
				
				this.isSelected = function(checkTab){
					return this.tab === checkTab;
				};
			},
			controllerAs: 'panels'
		};
	});
	
	app.directive('productTitle', function(){
		return {
			restrict: 'E', // E for HTML element
			templateUrl: 'product-title.html'
		};
	});
})();