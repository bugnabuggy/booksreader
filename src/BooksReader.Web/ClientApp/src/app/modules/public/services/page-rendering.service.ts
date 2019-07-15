import { Injectable, Compiler, ModuleWithComponentFactories, Component, ComponentFactory, NgModule, ViewContainerRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PublicPageService } from './public-page.service';
import { PublicModule } from '../public.module';

@Injectable({
  providedIn: 'root'
})
export class PageRenderingService {

  constructor(
    private compiler: Compiler,
    private publicPageSvc: PublicPageService
    ) { }

  compileTemplate(template: string, container: ViewContainerRef){
    let metadata = {
      selector: `runtime-component`,
      template: template
    };

    let factory = this.createComponentFactorySync(this.compiler, metadata, null);
  
    return container.createComponent(factory);
  }

  private createComponentFactorySync(compiler: Compiler, metadata: Component, componentClass: any): ComponentFactory<any> {
    const publicPageSvc = this.publicPageSvc;
    const cmpClass = componentClass || class RuntimeComponent { publicSvc: PublicPageService = publicPageSvc};
    const decoratedCmp = Component(metadata)(cmpClass);

    @NgModule({ imports: [CommonModule, PublicModule], declarations: [decoratedCmp] })
    class RuntimeComponentModule { }

    let module: ModuleWithComponentFactories<any> = compiler.compileModuleAndAllComponentsSync(RuntimeComponentModule);
    return module.componentFactories.find(f => f.componentType === decoratedCmp);
  }
  
}
