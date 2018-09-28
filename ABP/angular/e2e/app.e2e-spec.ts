import { ABPTemplatePage } from './app.po';

describe('ABP App', function() {
  let page: ABPTemplatePage;

  beforeEach(() => {
    page = new ABPTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
