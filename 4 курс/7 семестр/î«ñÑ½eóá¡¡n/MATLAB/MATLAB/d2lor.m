%Визначення порядку моделі d2
% iter - кількість ітерацій - довжина вектора станів
function d2=d2lor(iter)

dt=0.02;
[x y z]=lorenzo(iter,dt);  % Генерація вектору станів
d=3;
l2=iter-d; % макс. довжина підвектору для порівняння
vec=[x;y;z];

%Знаходження мінімума та максимума дистанції  між 2 підвекторами
max = 0;
min = 0;

for i=1:l2
    for j=1:l2
        sum = 0;
        for k = 1:d
            sum = sum + (vec(k,i) - vec(k,j)).^2; 
        end
        sum = sqrt(sum); % 
        if sum > max
            max = sum;
        end
        if i==1 && j==2
            min = sum;
        else
            if (sum < min) && (sum>0)
                min = sum;
            end
        end
    end
end

% Обчислення розподілу , скільки пар векторів мають дистанцю менше  epsilon 

scales = 18; %кількість діапазонів epsilon через *0,67
start = 1;
ratio = zeros(2,scales);
n=start;
epsilon = 1/(2^n);  
while epsilon*max>2*min && n<scales
    count = 0;
    for i=1:l2
        for j=1:l2
            sum = 0;
            for k = 1:d
                sum = sum + (vec(k,i) - vec(k,j)).^2;
            end
            sum = sqrt(sum);
            if sum < epsilon*max
                count = count + 1;   % власне, інтегрування
            end
        end
    end
    ratio(1,n) = epsilon*max;    % точки epsilon
    ratio(2,n) = count;      %той самий інтеграл від epsilon
    n=n+1;
    epsilon = 1/(1.5^n); % новий epsilon
end
%Графік 
[p q]=size(ratio);
loglog(ratio(1,:),ratio(2,:));
d2=polyfit(log(ratio(1,floor(q/3):ceil(2*q/3))),log(ratio(2,floor(q/3):ceil(2*q/3))),1);
%d2=polyfit(log(ratio(1,:)),log(ratio(2,:)),1);
