import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AccountService } from '../services/account-service';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const accountService = inject(AccountService);

  // 下面的操作，让user没有了响应性，只是一个普通的对象，而不是signal
  const user = accountService.currentUser();

  // 如果user存在，那么每个请求都在拦截器的设置下，携带Authorization的请求token
  // 记得在app.config 中记得注册拦截器
  if (user) {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${user.token}`,
      },
    });
  }

  return next(req);
};
