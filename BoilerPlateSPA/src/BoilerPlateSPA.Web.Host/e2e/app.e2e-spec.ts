import { BoilerPlateSPATemplatePage } from './app.po';

describe('BoilerPlateSPA App', function() {
  let page: BoilerPlateSPATemplatePage;

  beforeEach(() => {
    page = new BoilerPlateSPATemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
