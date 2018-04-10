import { NgModule } from '@angular/core';

/* constant */ import { Configuration } from 'app.constants';
/* module */ import { AuthModule } from 'core/auth/modules/auth.module';
/* service */ import { OidcSecurityService } from 'core/auth/services/oidc.security.service';
/* configuration */ import { OpenIDImplicitFlowConfiguration } from 'core/auth/modules/auth.configuration';
/* model */ import { AuthWellKnownEndpoints } from 'core/auth/models/auth.well-known-endpoints';


@NgModule({
    imports: [ AuthModule.forRoot() ],
    declarations: [ ],
    providers: [
        OidcSecurityService,
        Configuration
    ],
    exports: [ ]
})

export class CoreModuleShared {
    constructor(
        public oidcSecurityService: OidcSecurityService
    ) {
        const openIDImplicitFlowConfiguration = new OpenIDImplicitFlowConfiguration();

        openIDImplicitFlowConfiguration.stsServer = 'http://localhost:5050';
        openIDImplicitFlowConfiguration.redirect_url = 'http://localhost:8082';
        // The Client MUST validate that the aud (audience) Claim contains its client_id value registered at the Issuer identified by the iss (issuer) Claim as an audience.
        // The ID Token MUST be rejected if the ID Token does not list the Client as a valid audience, or if it contains additional audiences not trusted by the Client.
        openIDImplicitFlowConfiguration.client_id = 'singleapp';
        openIDImplicitFlowConfiguration.response_type = 'id_token token';
        openIDImplicitFlowConfiguration.scope = 'dataEventRecords openid';
        openIDImplicitFlowConfiguration.post_logout_redirect_uri = 'http://localhost:8082/Unauthorized';
        openIDImplicitFlowConfiguration.start_checksession = false;
        openIDImplicitFlowConfiguration.silent_renew = true;
        openIDImplicitFlowConfiguration.post_login_route = '/'; // !!!
        // HTTP 403
        openIDImplicitFlowConfiguration.forbidden_route = '/Forbidden';
        // HTTP 401
        openIDImplicitFlowConfiguration.unauthorized_route = '/Unauthorized';
        openIDImplicitFlowConfiguration.log_console_warning_active = true;
        openIDImplicitFlowConfiguration.log_console_debug_active = true;
        // id_token C8: The iat Claim can be used to reject tokens that were issued too far away from the current time,
        // limiting the amount of time that nonces need to be stored to prevent attacks.The acceptable range is Client specific.
        openIDImplicitFlowConfiguration.max_id_token_iat_offset_allowed_in_seconds = 10;

        const authWellKnownEndpoints = new AuthWellKnownEndpoints();
        authWellKnownEndpoints.issuer = 'http://localhost:5050';
        authWellKnownEndpoints.jwks_uri = 'http://localhost:5050/.well-known/openid-configuration/jwks';
        authWellKnownEndpoints.authorization_endpoint = 'http://localhost:5050/connect/authorize';
        authWellKnownEndpoints.token_endpoint = 'http://localhost:5050/connect/token';
        authWellKnownEndpoints.userinfo_endpoint = 'http://localhost:5050/connect/userinfo';
        authWellKnownEndpoints.end_session_endpoint = 'http://localhost:5050/connect/endsession';
        authWellKnownEndpoints.check_session_iframe = 'http://localhost:5050/connect/checksession';
        authWellKnownEndpoints.revocation_endpoint = 'http://localhost:5050/connect/revocation';
        authWellKnownEndpoints.introspection_endpoint = 'http://localhost:5050/connect/introspect';

        this.oidcSecurityService.setupModule(openIDImplicitFlowConfiguration, authWellKnownEndpoints);
    }
}
