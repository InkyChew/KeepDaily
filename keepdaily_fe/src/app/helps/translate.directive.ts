import { Directive, ElementRef, Renderer2 } from '@angular/core';
import { TranslateService } from '../services/translate.service';

@Directive({
  selector: '[txs]'
})
export class TranslateDirective {

  constructor(private elref: ElementRef,
    private renderer: Renderer2,
    private _service: TranslateService) {}

  ngOnInit() {
    const el = this.elref.nativeElement;
    const title = el.title;
    if(title) this.translateTitle(el, title);

    const nodes: NodeList = el.childNodes;
    nodes.forEach(node => {
      const text = node.textContent?.trim();
      const key = node.parentElement?.dataset['txs'] ?? text;
      this._service.curLang$.subscribe(lang => {
        if (key) {
          const translatedText = this._service.translate(lang, key) ?? text;
          this.renderer.setProperty(node, 'textContent', translatedText);
        }
      });
    });
  }

  private translateTitle(el: any, title: string) {
    this._service.curLang$.subscribe(lang => {
      const translatedText = this._service.translate(lang, title) ?? title;
      this.renderer.setProperty(el, 'title', translatedText);
    });
  }
}
