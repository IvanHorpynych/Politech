function wolfram;

%%% Wolfram cellular automaton  
%%% Didier Gonze
%%% Created: 27/7/2008
%%% Modified: 27/7/2008

clear;
clc;


%%% key parameters:

s=200;          % size of the grid (1 dim) - default: 200
n=100;          % number of initial living cells - default: 100
niter=100;      % number of iterations - default: 100
r=136;           % rule (integer between 1 and 256) - default: 90


%%% generate initial pattern:

%k=round(s/2);            % start with a single living cell
k=ceil(s.*rand(n,1));     % start with n living cells randomly distributed

M=zeros(niter+1,s);
M(1,k)=1;


%%% encode the rule

u=rule(r);


%%% run the game

for i=1:niter-1
   M=update(M,i,u);
end


%%% draw the figure

figure(1);
clf;
Mout=ones(niter+1,s)-M;
h=imagesc(Mout);
colormap(gray);
set(gca,'XTickLabel',{''})    % to remove x tick labels
set(gca,'YTickLabel',{''})    % to remove y tick labels
set(gca,'XTick',[])           % to remove x ticks
set(gca,'YTick',[])           % to remove y ticks
xlabel('Space  --->','fontsize',16)
ylabel('<---  Time','fontsize',16)

end


% ==================================================
% Update matrix
% ==================================================

function P=update(M,i,u);

v=M(i,:);
L=length(v);

w(1)=0;
for j=2:L-1
      w(j)=nextv(u,v(j-1),v(j),v(j+1));
end
w(L)=0;

P=M;

P(i+1,:)=w;

end


% ==================================================
% Rule
% ==================================================

function u=rule(r,a,b,c);

%%% rule (there are 2^8=256 rules in total)

for rule=1:256

for p0=0:1
for p1=0:1
for p2=0:1
for p3=0:1
for p4=0:1
for p5=0:1
for p6=0:1
for p7=0:1

    if (p0*2^0+p1*2^1+p2*2^2+p3*2^3+p4*2^4+p5*2^5+p6*2^6+p7*2^7) == r
        u=[p0 p1 p2 p3 p4 p5 p6 p7];
        break;
    end
    
end
end
end
end
end
end
end
end


end



end


% ==================================================
% Nextv
% ==================================================

function w=nextv(u,a,b,c);

z=100*a+10*b+1*c;

w=0;

if (z==0 & u(1)==1); w=1;
elseif (z==1 & u(2)==1); w=1;
elseif (z==10 & u(3)==1); w=1;
elseif (z==11 & u(4)==1); w=1;
elseif (z==100 & u(5)==1); w=1;
elseif (z==101 & u(6)==1); w=1;
elseif (z==110 & u(7)==1); w=1;
elseif (z==111 & u(8)==1); w=1;
end


end










