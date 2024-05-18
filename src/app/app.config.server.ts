import { mergeApplicationConfig, ApplicationConfig } from '@angular/core';
import { provideServerRendering } from '@angular/platform-server';

const serverConfig: ApplicationConfig = {
  providers: [
    provideServerRendering(), { provide: 'apiUrl', useValue: 'http://localhost:5058/api' }
  ]
};

export const config = mergeApplicationConfig(serverConfig);
