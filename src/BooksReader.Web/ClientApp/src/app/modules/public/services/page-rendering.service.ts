import { 
  Injectable, 
  Compiler, 
  ViewContainerRef, 
  Component, 
  ComponentFactory, 
  NgModule, 
  ModuleWithComponentFactories } from '@angular/core';
  
import { CommonModule } from '@angular/common';
import { PublicModule } from '../public.module';

@Injectable({
  providedIn: 'root'
})
export class PageRenderingService {

  constructor(
  ) { }

  compileTemplate(compiler: Compiler, template: string, container: ViewContainerRef){
    let metadata = {
      selector: `runtime-component`,
      template: template,
    };

    let factory = this.createComponentFactorySync(compiler, metadata, null);
  
    return container.createComponent(factory);
  }

  private createComponentFactorySync(compiler: Compiler, metadata: Component, componentClass: any): ComponentFactory<any> {
    const cmpClass = componentClass || class RuntimeComponent { };
    const decoratedCmp = Component(metadata)(cmpClass);

    @NgModule({ imports: [CommonModule, PublicModule], declarations: [decoratedCmp] })
    class RuntimeComponentModule { }

    let module: ModuleWithComponentFactories<any> = compiler.compileModuleAndAllComponentsSync(RuntimeComponentModule);
    return module.componentFactories.find(f => f.componentType === decoratedCmp);
  }
}
