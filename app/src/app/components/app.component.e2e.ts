describe('App', function() {

  beforeEach(function() {
      browser.get('');
  });

  it('should have a title', function() {
      expect(browser.getTitle()).toEqual('uManage');
  });

  it('should have <nav>', function() {
      expect(element(by.css('umanage-app umanage-navbar nav')).isPresent()).toEqual(true);
  });

  it('should have correct nav text for Dashboard', function() {
      expect(element(by.css('umanage-app umanage-navbar nav a:first-child')).getText()).toEqual('DASHBOARD');
  });

});
