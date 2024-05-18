import { ApplicationConfig } from '@angular/core';

interface ApiUrlProvider {
    provide: string;
    useValue: string;
  }
  
  export function getApiUrl(config: { providers: any[] }): string {
    const provider = config.providers.find((provider): provider is ApiUrlProvider => 
      (provider as ApiUrlProvider).provide === 'apiUrl');
    
    return provider ? provider.useValue : '';
  }
