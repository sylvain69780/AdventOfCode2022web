namespace Tests
{
    public class MonkeyMathTests2
    {
        [SetUp]
        public void Setup()
        {
        }

        public void MonkeyMath()
        {
            var solver = new MonkeyMathSolution();
            var input1 = @"root: pppw + sjmn
dbpl: 5
cczh: sllz + lgvd
zczc: 2
ptdq: humn - dvpt
dvpt: 3
lfqf: 4
humn: 5
ljgn: 2
sjmn: drzm * dbpl
sllz: 4
pppw: cczh / lfqf
lgvd: ljgn * ptdq
drzm: hmdt - zczc
hmdt: 32";

            var input2 = @"lfrh: 2
tctr: vzhm - qtbn
smdc: vlzv * jdlj
jcph: vrdq + rzvq
gsds: jpch + wfpj
hpqm: vjzp * dnzm
gdbq: gptj * rdcl
hsdr: zjnq + qrzb
jwhr: 15
gsvq: 2
lwgs: vjjm * pclt
bdjp: 14
lgbd: 3
zpvj: lzgm * gfcg
mhjg: 2
pjsc: tvqp - tvws
fmdp: bfrr * jvsj
vvvt: 2
vbpf: bszc * qvdg
gcqp: 3
wvmh: vzrw + bffp
blhv: 2
vlcb: 13
zfcc: 2
vzww: mrhw + mwzv
npgb: bfcw * gqbp
cqwv: zlsl * qcgr
lmbr: sprw * cjnt
cfzp: 9
pnvb: 2
zqwh: 3
pghh: cjfv * whzt
mrmm: 3
nmwl: nffr + dssp
zfhl: vfqb * fpnc
rlvn: 2
hgft: 4
hhjz: 3
ljfd: vphz * jntq
cnrj: 6
plnm: cpcl * hblz
sdbh: 6
hznw: 2
rbfq: 3
vzhm: schw / mgmf
hjmj: wlzd / ffrl
wjwd: fcnr - djfc
tvvg: 3
bffp: gwnd + tmzz
bblr: dwtq * hbht
jrdb: 5
mzvb: 5
zjnq: mvhh + sbfm
mjlp: 6
fzfd: wsvj * bjbl
vfts: 1
slrt: 5
jtjj: 17
rrqg: cbmz * svmw
mgln: 4
bhrt: vrns * mzfp
cfwn: 5
ljdt: 5
wbqn: zhbt + wvmh
mrzq: hbfr * dqfg
sjjr: 6
grqd: mlgb / bqqb
pjgr: 3
qwmh: nvrp * rcmf
hdtn: 4
rwjv: 2
bzhz: jwzp * fwsf
bbjv: mbdg * mblh
sbfm: nqvg * rgmw
jmpr: vhdq + plmw
vchq: 5
hjld: 2
pdqq: bccr * wprn
qtfs: 3
slnh: 2
mgmf: 4
lfgt: rhfc + mcvj
rgcr: zpqg + zmvt
qqmp: cwgd / zrmg
rhfc: 17
thrp: 3
qvgr: rvjn * tgvf
mbdg: 5
rhbr: zvlr * swjv
smrm: 3
wmrd: 2
gmbt: 11
vjnw: dclh + cdqq
jjqw: 4
fdtg: 5
dwtp: 15
fftf: mzvb * nhdf
jcjq: 4
lchh: pqzm * lwms
zrts: 2
dqzj: cwhc * rlvn
jbbr: 6
pgcb: jvmh * prnw
ndds: jfbl + vcdn
tvmm: fcml * zdgb
rzvq: bprb * bwhf
spjc: fwgp * jtjq
trns: dprf + rpml
dbgc: cnrz * mhcc
cqbn: 2
dvdh: qtjr * bcjr
gdnq: 4
hrgn: pctw * dzrh
blvv: bhff - gwst
mcrg: 8
rdzt: fbvv * qjsq
pmdl: wffq - mnln
jlzc: 2
vwqt: 3
ddjs: brlc / stln
njjn: dqdq + bzhz
tdvb: 3
dlrq: 1
wmps: 2
twrb: 2
srrm: pmwj * ccsl
tprs: tnjl + bplf
ltnm: wddp * jjqw
qhrc: 1
rmqs: tvhd * qqmp
zldc: 2
rppw: psqr * fgpl
vrns: 5
ntwt: 2
lvqc: 5
hmgj: 5
drjs: 5
brmq: vrhz - vqrm
zlsl: 2
mtgb: 2
wrjc: 2
jsvm: rdzt + mgms
dnqj: thgb / bvlc
dhvh: hwmn + bmqq
wzzn: 1
hdzj: scnb * sgnz
vhrp: zrfv + rslc
hjjp: lwcb + zggc
hjvl: sthz * njrn
qqgp: 8
vqtl: tmbp + nvnr
zmfs: 11
vlth: 1
hbfr: 3
hzhd: 19
zzsp: vhqm * dggl
rrmb: 2
grgg: 3
dvjz: 11
hhjw: 7
gnnn: 3
snjz: 6
pppv: 7
cfnh: lllj + wfwd
gzpd: 2
jwsd: 3
vqvf: zrwp / mtgb
pmzv: 4
mwdv: 2
jrhg: lvlb + zslv
gcqw: 3
gptj: 2
ctvz: wndz * bwjc
mfld: 2
tsgd: 17
lrdm: zbtr * jfmv
rttr: 2
bfcw: ztsc * rdrd
bjbl: 2
zqvf: rphg + vfrh
nfbq: 2
dndn: 2
thzd: 4
qtjr: ftbg * hdtn
znbp: gvcq * wgzz
fdhh: vvvc * rfdr
fdnn: qdsj * pppv
wlds: 3
dflt: 2
rnlm: 5
llmd: zlpt + llrn
svtv: trns * cjwp
tmgc: nhjc * tbwf
bvmr: 2
pwsv: 4
fgvh: bsqw * tgls
stdj: rfsq / mhjg
rpqb: ptfs * tcpn
lqpq: lzbr * wdpm
jrvv: vdqn + gdgh
sghs: fbjr + fqzq
lbtl: rgnc + hdjc
brhw: njvb * mdmr
llnz: pjgr * lqvr
humn: 2820
hflj: 7
qlpt: svwh * ngcc
jgwv: 8
hjtn: gqnj * rphf
bcvs: qmsj + lrtb
djfc: sgrr * njbv
gdgh: 14
vcdn: 1
blcr: zghw / mwnm
wgzz: 2
mrgl: 3
vbqd: 2
bltg: 11
plpf: 2
zlpt: 13
jqzw: 2
dnvz: frqf + fzfd
vwwn: tgtq * wcnm
tzws: nbjc - smzs
ltwd: 2
ccwj: mfjt * bdpw
ljzt: swbr * rfjz
swsb: jgrb + hwqn
gjhb: sbmj * lgsh
fgwv: 13
rsrd: 2
pqzm: 2
tcwc: 13
fbvh: crrr - fgvh
nsqm: wrjv * wtng
vhdq: jmbw * tcpj
nqjr: mfsw + zfhl
fznr: btnz + slnh
tsmd: 4
vtjp: wfrz + hnzn
vfld: rvvl + twrb
qnhb: qtzp * fznr
vtfw: gsdt + tcwc
fttw: fstd / qpdj
bjct: cdgr - fchd
rszw: 3
zmdq: 2
lzbr: vqvf + tmlz
slts: 10
vnps: jzrg + rdsl
ffrl: 2
pnzb: gndq + sfvv
hwdf: 4
rnnh: 3
swbr: 17
bvdl: 6
jpch: 5
whjr: hddj / pllm
ccwg: njjs + hcvm
cprn: 2
mtpd: 4
tvws: rgjt * fwqm
bwlh: 10
zjdj: 5
trlr: rcnc * pjnl
fqzq: qmnd + phtd
zmwd: csrw + whnl
sgrr: 3
rdjb: 2
wfpj: 4
fbvg: 3
mtmg: zbhw * qfmd
jjcn: rclc * rzhf
ccsl: 5
rtcp: bmhb * shpp
rrcb: 2
rdrd: sbql + lpwl
wsvj: dsnp + tjrp
lgsh: 10
mmvh: pzqf * jltn
fprh: 3
lfvl: wtvg * vsnb
zggc: gqfw * trnh
wprn: 2
hqnd: tdvb * mtwq
sqzf: znbp - mdpb
mnln: 4
prnw: dlhd * cfwn
gfbn: 4
dfbv: 2
lhgq: 2
zljc: 7
vdqn: bzdc * dzvn
jdlj: 2
dgdw: vsjh * wrjc
sdmw: zvln * jhfw
nrgr: 20
dtmq: sqjc * cznn
wfbn: rrcb * pwzf
dphv: rlng + gmbt
bvvb: 9
wlnh: ccgp + nldd
whnh: 4
bhjz: wfgl + bbqz
bmqq: 11
rgbj: pdqq + vjnw
fwsc: dhrl * dcdh
cbmz: jsvm * jrdb
jnwt: 6
tbsz: phps + zrjz
zcvt: 2
qtff: 8
hsmq: wzwb + llnz
hvqs: 3
zbtr: 2
bcpt: 2
rqrd: 3
bfvh: dphw * cbls
dnzm: 5
cltm: rrmr * qglq
nqlh: nmwl / nfbq
hcjt: zrlf + tqds
tcqp: slls * wlds
nqbt: vffg * svml
wnls: 3
grhv: 2
cdqp: hlcg * hrfh
njrn: 7
trtp: rmqf * bvdl
gfcg: ncsw * qrds
dftt: rjvc + qcsf
scgq: 10
hjqj: 2
wfwd: vbcs * nsjf
zntw: nwqj * gznn
wfsv: 1
mfgl: ffmn + gbsf
cdbd: pdjt * phdb
mdwt: 9
tcng: 9
ghff: 4
rhjb: 2
wqmr: rtdt + jrvv
hscm: 19
zhbt: nprf * hntn
svwh: 8
mpsl: qrzs / ljdt
bvzr: 5
zrjz: fpzh * nwtw
hcnz: nczn * rjss
qtfd: 2
rhqg: hfdh + qghj
phps: hdzj * bslm
crdb: ztwn * lqgb
nmjj: ltdd * fbft
dnqv: 4
rphg: 1
grnd: fvmj * prrc
hrfh: 2
dggl: 19
hntn: 3
shqv: ssps * fvqd
bzdc: 2
tmbp: qqnl * fcgf
zmlv: wjcm + mjqg
ccgp: nqbt + cdtr
swjv: 8
ccst: cjsg / csgj
qlzg: zwbp + ztlf
ndtt: 2
hqlg: dwpj + cnlh
plmw: 5
rgnc: 2
bplf: tbvg / jtwm
stln: 2
zmgp: 2
gdnn: 3
rzhf: smng + nsvr
dqfg: 3
htgv: zqjt / lqjv
bszc: 2
prpv: 14
qcgr: 3
sshh: qsql * vlzq
grhg: hwpj * gwdv
szrs: dflt * sghr
lfnp: 8
rpjs: 2
tpvf: 2
nspf: pnbb * tgdq
hnvc: 2
njmd: 4
lpwl: nrrd * jqzw
wtcj: 5
rmqf: 3
hvbm: 4
qtzp: 4
vgzw: 4
lqgb: 4
vzrw: 4
mjwh: 2
hmvl: 1
rtdt: zdmq * lgtt
gwnd: 5
zdzr: 3
wqmc: rpbh * brmq
fcsq: nwnp * pbrs
thvd: 7
vqlm: 19
nwnp: hqvh + mbqh
zdbw: vlth + ndrw
hbht: 7
ctbl: dfbv * jtjj
hmwp: 3
hblz: mmdc * nzqw
fjvf: 9
zcwz: htgv * hnbc
zrtl: 7
qcsh: 4
gfbl: 3
jgrw: jdbz * cvht
gvcv: 13
htsg: 2
dzcz: pldv * blhv
mfmr: 7
wdpm: bdrs + dhdn
mmhn: pdvr * lpzv
rzmr: zzrj * lcqs
bwhf: vfts + tpnv
lnvp: 11
qjbc: tsgd * rwzr
gdwj: 9
fqhz: zvjz * fjhs
wnvv: fgbv * spjc
sprw: vtjp + nsrq
rpvn: 2
nmms: cltm - jtwp
zdgb: jnsc + nsrj
msjv: nwnn * zspd
gjbs: fgfr * vwsh
jdbz: 16
mqgm: vwqt * ldbj
qqgz: ncvq + vpwh
lqls: lsfl * qsmn
fbqz: wpmw + njmd
jtjq: 5
ddsr: vvvt * qcmm
vfrh: pctc * bznm
shvg: 7
vfqb: 5
gdsj: 2
pzsw: bphq * hnrz
zlmq: gtjh * smrm
npwd: 2
mjqg: rsrd * htch
rwrs: vcwv * pdhf
bpjw: cmfb * gszg
jfvm: lnvw + mwfr
pclt: 2
mmpr: 3
rlng: wztw * cjrz
ptwf: sdnh / wfzq
slls: 3
blmd: rmzb * hqhc
crrr: dftt * nmjj
zvqj: 7
lmjq: 1
vvvc: 3
ppvs: wqmr * svmj
lwms: jqqj * bltg
qlqd: jtfd + gfbn
rdcl: prqg + bvvb
rhcs: mrgl * ccwg
wbnz: 19
hnzt: 5
wrgl: 2
nctf: 4
frgv: sbzt + spfm
shwn: 2
tgdq: 2
hbll: 3
fzsw: vflf + nlmd
mpjr: 2
hpqv: 2
ncwg: gplg * bhjz
lsfl: 2
dcrt: 5
ffmn: vqms * jdfq
nvnr: dqzj * rtgt
dttr: 11
qglq: tnbg + zshl
fzms: vqtl * qhpl
hmtl: 20
tppv: 2
rgvf: pmdl + njbn
wdrs: mmhn + vzgr
cznn: 5
jdfq: 2
ctsv: 5
lfqp: 5
jpzl: 2
wmjj: 7
schw: rdvv * trtp
sqwd: 15
mlgb: bnjd + cdrv
fwgp: 5
szml: hggs * lqjl
spzb: vgwl + lphj
rpbh: fbqz * fqhz
dhrl: vnjs * frvj
rlnb: vbrd * tmgc
gblm: qbwf * ctsv
zspj: dmws * rhcs
brvh: cbdq * mmpr
jjnl: 2
lzrh: hlgt * qztq
nblv: 2
qjwh: ltvp * pqpl
cqmh: 6
pdhf: wjdh * hqnd
gdjl: tdpn + srrd
ctrv: 3
mzfp: jqnt * tprs
sjhz: zgml * dwtp
zvvv: slzm * lfnp
zvzn: 18
lczp: 3
prrc: 7
tpmd: fpll * lqzz
jnjz: pctp * ntzh
zgml: 3
bfcz: plsp * jphn
gpvw: tcpp + vrjn
vngl: ptnz * tvvg
zlfr: zcdr * drjs
rjvq: 15
lpjt: ndtf + wbqn
pbrs: 4
hqvh: 6
cjwp: 2
rmvs: vrnf * jjnl
svmj: 3
ftpr: msjv + tpmd
ldtq: wggt * cbwn
wjdh: 2
bcjr: 4
ldbj: mzgc + wzzn
dcdh: 3
zqmz: rhvs * dnbh
cdgr: hggv * jlrd
rnqr: 4
gsdt: 6
nwqj: mdpl + hmtl
qwjr: 3
brpw: lfvl * nttq
znfm: 1
ztwn: 2
qqwm: cwtt + hvqs
ngwd: vwfl + jnwt
ppns: 3
zmzp: 2
wfrz: 9
ttpl: htfl - dgjp
rfhd: 1
dqwq: 8
qngf: rfhd + cnrj
mmdc: 2
cdqq: vvff * hmvf
nttq: mmvh + nbcc
qtdr: ctrv * jvtt
blpn: prcf * nghv
hgqz: 10
tssw: 2
cgft: zwbc + njzj
cjsg: jrqn - dbmz
qrds: jhrw + tcng
fbzv: vlnv + dsqg
nsrq: tdvn * hpqv
jntq: 2
cndn: jsrh / jsbz
wjcm: 1
gvcq: zmhl + wfrd
wsvh: 5
zmvt: fprh * hbjd
rphf: 2
hrbp: bclp * rgrd
bdmt: 4
nfgc: htsg * nlsl
frnd: 2
fpzh: 15
dsqg: 8
vccp: 2
hvph: fzvc * jnnm
rdlf: rwfh * shvg
njfj: mpjr * zvvv
hbfj: 2
dbtp: 8
cttj: 13
nldv: prpv / zcvt
djlz: 2
psht: ldtq / rtmm
vbmt: lqhv + ltnm
tqfw: 17
mtbz: hptq + dqwd
vbwd: 4
ncsw: 3
cpmg: 4
vrjn: jtfp * qrwj
mfjt: jlwc + cfwp
vrdq: mpsl / gpqv
gqbw: 2
lwcl: 2
zqjt: dszq * hwdf
nhdf: 2
spqc: hlcl - tzds
rjmh: 2
mclp: wghm + whzr
gwst: cpvr * jfwn
svgz: 1
lnvw: fdhh * gqbw
qdsj: 5
bpzv: 3
hqbm: 2
qvrr: nfgc + sjhz
frvc: 4
wzqh: 2
tjrt: hsmd * dnfb
lpzv: 2
vlzv: prmm * srzn
mstb: sjnz * lvqc
wfgl: fmfd * tlbz
lqzz: 2
tmrv: jnsn * spqc
lgtt: 5
vsjh: tlzl * gnzj
hbjd: qnml + wzcs
zjlt: 7
tjvc: ghsc - dttr
njvb: 3
llss: 2
tdpn: hqbm * zjfg
dqdq: ljsn * cdqp
jqrw: 5
bsqw: hghw + cpff
qsql: zljc + pwsv
cjwv: 3
dljv: 4
qmpl: 2
mfsw: 1
dnss: hwwg * lbtl
nffr: brws * szmd
mdmr: 3
zwbp: rdft * rspn
ncwn: szvg * bdmt
dcwb: tczf * bpzv
qrzs: hpqm * gqjq
zqdv: 4
trnh: 4
jbjv: msrs - mzdv
gnjb: tsbh + wgpp
tcpj: 19
wdrd: 11
sgnz: nrcl + grnd
dcfh: 1
wwpg: 3
flpp: 17
ssfv: 6
wjgv: 2
qrpq: 18
hsrz: dlrq + lqls
fbjr: 2
vhhz: 2
fcnr: jcph / pgll
qwwz: 2
fgfr: 3
tlvr: 3
dbsh: 4
qjsq: 7
tgtq: 6
mdrq: 4
wgrr: 4
ljnh: bpjw * fpzb
rfjz: 3
vrgh: qwvc + mcns
jtfp: 5
hsgh: 3
rcmf: 5
wggt: 7
hvrb: 2
tzds: 1
nrnp: trlr * hvbm
rpml: jlzr + ctbl
vgvz: 3
jnsc: 3
pdpc: 13
mjdf: fzmj + dnss
fbvv: 2
wzwb: 2
dfmj: zlfr + lqqq
qfmd: 4
pzqs: 5
qrtb: 2
lphj: 4
hqhc: 5
wpmw: 4
pgjs: 3
qqnl: gcqp * vncb
htbf: lclr * tlhv
zslv: bhtw - grhg
fwsf: blpn + tjft
zghw: wjwv + njvq
tsjc: 5
rslc: hhjz * lvnd
msrs: jnlp + hwmm
fgpl: pnfz * hflj
lhqg: tqgz / hltb
crmv: 13
rspn: 2
rvwm: 7
ffjg: 2
szdn: gvjj + mzjz
nfvp: vdhh * gglq
tbrr: 3
nzqw: svtv + gtrg
mlsb: thvd + hjlr
ftnv: npzn + lfgt
ndts: 2
hdll: zrts + lpjt
nqfz: 2
wndz: 5
wgpp: 4
zzwn: hrgn + vwwn
gbtd: 3
btpn: 2
fhdh: vbmt / qrtb
wcnm: tbrr * dbsh
zwqt: bpdh * vbjg
cwgd: gnrt + vgfr
jsbz: 2
bfqq: 2
zzjd: 3
nbds: 4
hdjc: llzw * qncn
dtzf: jlzc * wcdz
cqdc: svcv * brdf
gnhh: ppzd * lwcl
cspf: 4
trsr: 2
cpcl: 2
dwpj: 13
ztlf: ttnc * qqgz
wztw: 2
gqfw: cvcw * cfwv
tsbh: vnjc * pbjb
svmw: zjlt * qvrr
tmlz: cgft + gdnq
zzsh: 2
vpwh: jdrd * lmsw
hwwg: 3
hqqd: 5
vwft: 11
tslp: lhgq * hjjp
bnjd: lcgq * sqzf
rfdr: plnm + cnzp
fmcj: 2
tlzl: 3
qscj: 5
bqmz: qjmr * hsrz
vwfl: 5
frqf: ddqt + rrjf
cpcb: sshh + dzcz
cfzm: gmgl * cbpf
qmnd: qwjr * vhhz
jjgz: 2
zbhw: lhcj / rwjv
wjdt: 2
wffq: 14
nrdc: fttw + rcsf
cdtr: qjbc / djlz
mcvj: 14
lctv: ccqv + rmqs
czlt: 5
qcmm: 14
pnfz: 9
vrlp: zvqj * bfrt
prtj: 6
vbjg: tnql / dtnc
qwvc: pfgq / mjwh
pdvr: qvgr / cspf
bvlc: 2
cnth: 18
pndd: vczv * cndn
zzpb: 3
tzbs: 6
rrrv: pzgq + llqp
dsnp: tsjc * zmfs
dqvl: gdzf * szwb
lqjl: 3
gqnj: 4
rcsf: rzmr + jjcn
wsqf: 9
qfbj: 7
mrhw: cftb * jgwm
qtpn: 13
nqtc: 2
ppgr: 3
rvvn: ngwd + cnth
wcdz: 3
vsjg: wrrs * thrp
hddj: dzlj + dgbw
lqhv: bjct * rplr
jmgt: njfj + dmhj
bmpv: dbpc + bbjv
qwzp: shjj + blhs
qqzm: jcqz + gjbs
rnzw: 4
tpvl: 5
mhcc: 4
cjnt: 3
cvcw: 19
mdcs: 2
dcsl: fncg - zjdj
lpvg: pchp * zldc
gplg: 3
mmth: 2
zvcl: ppvs + mtmg
zfcr: vccp * mbml
whzt: bclt + gdwj
root: lvvf + rqgq
gvcj: 3
rfsq: vlrs * ftpr
crlb: qbsh + tqwt
gszg: 3
zpdw: 5
ltdd: 2
bdpw: plvm + ftnv
szmd: 2
cwsn: 2
nbcc: dbgc - rtlp
gznn: qwmh + fjbb
pmmw: qwwz * jmgt
nwnn: gsds - npwd
mwfz: rppw + rdfv
hggs: 3
lqqq: 3
hctg: 4
zjgh: 11
qmsj: wgff * mwdv
fnsd: 2
tlhv: wcdq + rpqb
dbmz: blcr * dvnw
qhsh: lwgs / pnvb
jlwc: hjmj * wjsw
wtng: pdsr + nmnt
pbdf: 6
lfsm: mfgl * zvcl
wddp: lrdm / mmcv
vqms: wfsv + snjz
ddqt: vrgh + vrlp
gtjh: 3
dfhg: dbtp * twcd
pchp: hhjw * dphv
dmws: cnpn * mrmm
htch: 7
fcml: 6
jqnt: 5
mtnp: 5
nshb: zwnr + zspj
lhcj: gnhh * gzpd
fnpl: mnsb + mcrg
fzvc: 2
jcqz: 1
vlnv: hvph + bbpz
swmj: jphw - qrpq
mqhh: 4
tjft: wsvh + ndtt
hjlr: 10
gmgl: hgft * jpzl
blhs: 5
rqrl: nnpm + mmth
fbft: 4
zrmg: 3
glqv: 2
vjwc: 3
pdtj: jzhc - wmjg
jnsn: zvzn + nsjg
vfcl: 20
jhjl: jqrw * gsvq
psqr: 3
hnrz: 3
nmnt: 6
lclr: 3
vhqm: 14
llqp: 8
vjjm: nrnp + rvtz
lwcb: rmvs + dhvh
cfwp: mfbz / bgbr
mblh: qdqg + sngl
qghj: 5
hmvf: 2
qdwd: nfvp - lftj
dnfb: fbzv - rdlf
rgrd: 3
zfrs: dcrt * rwnd
qjfv: 2
rbdj: smdc * rttr
tfvc: 3
qjmr: 2
nsrj: jbjv * nblv
hlcl: gnjb / trsr
hwmt: 5
jgrb: mtrn * ltwd
pfgq: qmpl * qsmg
hnbc: cpmg + vsjg
fqcw: tpvl * vlpp
jfmv: rgnq + bqmz
jzhc: wnvv / fjwf
pnbb: bqct - znfm
wtvg: 7
tsqv: cfnh / crdb
lmsw: bvzr + nmrp
zzrj: 11
grnm: tcrm * pbhv
cjfv: cfzp - gvcj
dtfw: 4
jhsl: dnqj - mrqj
ccjt: 3
nssz: 5
prqg: 2
jfwn: crmv * tppv
slzm: 4
cwhc: 17
bclt: 2
htfl: tssw * gfbs
hqmq: tlvr * tnlg
zsrr: hszs - lrqr
dzlj: wlnh - dzbn
ldmj: 5
tmpw: hwmt + qhrc
tvcf: 3
gsdm: nctf + rjvq
zrwp: qgfb / lfqp
ndtf: 9
shjp: rqrl + cfzm
zrrv: cwsn * tpfs
rfbf: 6
pctw: 11
pllm: 2
lrjl: tzbs + fqcw
hltb: 2
bmhb: 5
jtwp: pvnt / slrt
rdsl: pdtj + zjgh
vswr: srrm * lmdg
tqgz: jhhg * qlqd
zjfg: 19
nsjg: hnzt * tfnf
lqzb: jpwv + ljnh
jfbl: bgcf * rrmb
dwtq: 5
lmdg: 15
zgdd: hrqc + jwsd
vchf: 4
jdrd: grlq * qgvj
wgff: 5
bmtp: 2
jwwp: 14
sbmj: 5
rhtg: 8
thjw: 3
vlrs: 2
lrrv: 7
jvsj: 2
bqqb: 2
srvl: mlhh - dnvz
fqff: cbwr * wdbq
nghv: 2
qpcl: 2
dmst: lzrh / wzqh
nvrl: 3
qfmj: 5
ldtt: 3
nzpt: nlzs + rpjs
cbpf: 3
hlgv: 13
cdpd: ppgr * bqdf
plvm: sdmw / gzpn
trqr: 2
rqgq: ccwj * wqmc
tjnc: 2
sfvv: 1
rvwg: 4
dssp: hjtn * qhcs
vwsh: cttj * hbfj
tbvg: dfng * fgsw
sghr: qnhb / hctg
fdmq: nqtc * djfq
rwfh: 2
wcwn: 8
btps: 2
bwnq: 2
jbqz: zrtl + vfds
jvtt: 19
dzrh: rjqq + tvjq
bnmr: jwlh * cgvt
gwcm: 9
dlhd: 2
gqjq: pjsc + zzsp
cftb: 2
tmts: nvrl * fgwv
smth: stdj + grqd
vjzp: 2
hrqc: 4
qcsf: vjqt / zdzr
njjs: hrbp * shrr
nprf: 7
mtrn: 3
fcgf: njzb / jzqv
ssps: 2
phtd: 5
vpch: 5
vlzq: 7
fdpc: 5
jrqp: 1
srzn: frnd * hnds
mwzv: lscv * qngf
bqhj: 14
npzn: fwnr * mgpc
dzvn: rsmn * gdnn
jqqj: fddq / ssfv
cgvt: lmjq + rqnh
nppf: lqpq - tjrt
lllj: qfbj * vwft
njzb: wsqf * cjlg
lqbg: hggc + zcwz
blhn: 3
jlrd: 3
njbv: pdpc * vlcb
jlzr: zfqn - svgz
gzpn: 4
ptrw: 2
cnzp: tmrv * lggd
prmm: 4
lqvr: 3
zrlf: 3
whgw: 3
gfbs: cccg + zfrj
twcd: 17
wmwt: 1
mzqf: 3
gpqv: 2
jvmh: lfrh + zgdd
sfbr: 14
rgmw: znpd * hqlg
vgfr: lhqg + hscm
mzdv: 20
vbtp: 2
jrqn: fnsd * zfcr
svfz: sjsn * hpdj
hsmd: 3
qgvj: 4
jdmf: qvfc * vpch
hggv: whjr - pmmw
rjqq: 1
hfdg: nsqm + mzqf
dqwd: lswl / vqzn
fjwf: 2
tvjq: 10
dbsg: cvmb + ntlh
ntpp: gtrh / fmcj
tgls: prbd * mtnp
wjsw: pmlp * dnqv
grcz: wdrs * jjgz
dprf: lltv * dndn
fncg: nrgr + jwwp
gwvz: ppns * djls
bdtv: djwm * ccst
brws: fhdh - hcnz
wwqc: ntwt * tsqv
rdvv: ltfc * dwfc
zshl: dtmq * cqdc
nddz: fgjf * lgbd
bftw: fwsc + pndd
hldc: 2
qmhv: 1
bdrs: 1
tnbg: nfsp * qdwd
shpm: fzms + srpw
qtdz: 1
jnlp: zqmz - crlb
thgb: rtcp + gwvz
pbhv: 2
rjss: 7
wlzd: nppf * ltlj
bcbn: 2
dszq: hshg * fwrb
mfbz: grlr * lchh
wbzc: 3
jhgj: 1
mnsb: 15
vsnb: 2
fzmj: swsb + qppw
rtmm: 2
rmzb: 2
rvtz: spzb + hjvl
cpvr: 4
btmb: qgrv * ljfd
djfq: cdbd + bhfv
pbjb: 4
rqnh: mvpv * thzd
tcpp: tmts - qwdl
bslm: npbm + thjw
tgsr: 3
cfwv: 3
pjnl: 3
tlbz: lnpr + pgcb
qnml: 11
hlgt: 2
dhdn: 6
srnn: 2
sfsn: dbsg + dgdw
pzqf: 2
lswl: czgw * grnm
lzgm: 14
rgjt: 2
vnjs: tmpw + qtdz
vtbj: fdpc + wmwt
rrmr: tslp * pdmr
vlfz: 2
hwqn: whsq + lftw
dgbw: jrhg / wbzc
gtrg: pwmd * zmwd
scsn: 8
zmhl: fbvh / mfld
vfds: 2
zrfv: ffjg * dtmd
mbqh: 7
hdsz: pjpg - hmvl
qwdl: 3
bfrt: bnmr / qtfd
cftr: 3
bbpz: ftqv + hjld
jhhg: 4
fddq: pbdf * rrrv
zgwh: wjwd * qsqc
qhcs: swmj + qhvw
ftbf: nwcs / pjbg
frvj: 9
qqsv: mgln + vwqh
rjvc: tfvc * bmpv
clmg: cdpd * rgcr
smzs: nspf * cwqd
hszs: bcvs / hjqj
vntb: 1
wrjv: 3
vvcm: zmzp + slts
tfjp: 3
tfnf: 5
wttp: tfjp * ljzt
gtcf: wmrd * gdjl
bnpm: zcvp * rnqr
ltfc: 2
vdbj: wwqc / bcpt
rnnd: cqmh + qfwd
tqwt: 1
tpfs: shnr + dfmj
jphw: rnnd * hmgj
lvlb: qhsh * zfcc
zhcg: 3
fjhs: 2
jgwm: qlpt + tctr
zwbc: 19
mbml: pghh + rgvf
wfrd: vlfz * mtbz
nvrp: 5
vdpd: 7
rrjf: gtcf / ptrw
mgms: lczp * blhn
jspd: 2
nrrd: shqv - vntb
dtmd: mrzq + frvc
tczf: 12
tpnv: qqgp + vfcl
zdcv: tjvc - fmdp
fwjl: 2
nldd: qtcq + jrqp
tgvf: 4
gdzf: 2
drvj: 20
vgwl: 2
szvg: qwhc - fdtg
gndq: 19
bclp: 3
hptq: rhqg + dqwq
qgfb: dcsl * jhjl
zpqg: llmd * qhsf
ccqv: wwpg * dcnm
nlsl: mstb + hdnj
sdnh: bdtv + hcnj
ttcc: vbqd * hfgh
mvpv: vbwd + wqrh
ccwt: 2
dwfc: vqlm + lctv
ldww: hfdg + blmd
dbvc: 17
qsmg: btpn * hsmq
wdbq: 2
ppzd: 4
ntlh: 1
qztq: dqvl / hznw
qpdj: 2
fvqd: 6
ptnz: 2
ftqv: gbtd + vvcm
nlmd: zmlv + lpvg
dgjp: grcz + ttcc
srpw: vzww + jnjz
dclp: 2
rclc: 4
nrcl: wgsr * llss
cnlh: 10
bgcf: 3
tcpn: 2
vspl: bfqq + mfmr
bqdf: 2
ltps: 1
dfng: zmdq * bnpm
fstd: fdmq - brpw
bfrr: ngtn + hgqz
llrn: cgbn * wrgl
mpvs: rhjb * pzsw
jtwm: 2
mjth: sfbr / nqfz
wngz: 1
tqds: ghff * jsnt
tnlg: tzws * vchq
ztsc: 2
rwnd: 2
dcnm: ldww / dvhd
wzcs: 6
njzj: 6
qbwf: gsdm + mdwn
sngl: qjfv * cqwv
llzw: 3
zwgv: 7
lrtb: bdjp * mjlp
ndrw: 6
cbdq: tjnc + vspl
jzrg: blwz + qfmj
lvvf: rhtg * nmms
vqrm: mdcs * lfsm
ngcc: dtzf + pgwt
rdft: nqlh - tvmm
rdfv: cbpw * bmtp
lrlm: 6
rrnh: 7
qvdg: 6
phdb: 5
tgng: 3
jhrw: 4
plnt: 3
fgbv: 2
qwhc: dljv + rbdj
tnjl: 7
cfrh: 4
lltv: tsmd * mtpd
shrr: 2
vdhh: 3
nwtw: zfrs + qtfs
sjnz: rhbj + tccp
csgj: 6
wcdq: ldmj * vgvz
bdmv: gcqw + lrlm
btnz: 15
lcqs: 5
fwnr: 5
fpzb: 3
whzr: gthg * htbf
dzbn: sqwd + bfcz
cjlg: 15
vjsz: 5
jltn: 11
sthz: 5
svml: brvh + rnzw
cvmb: 12
gglq: jfvm + rrqg
fgsw: 2
sjbc: hqmq / ccjt
znpd: 19
cgbn: 5
lqjv: 4
nfsp: ndds * szdn
cccg: zmgp * dgqj
cmfb: gdbq / btps
vphz: 11
qzrn: 7
bccr: bqhj - vjwc
ttnc: 2
cwqd: 2
djwm: 3
nczn: gjfw * gtfs
nmrp: cppg * fnpl
plwb: 2
pglg: fzcp * zcvl
czgw: 3
svcv: 2
hnds: 4
pctc: 5
ggwb: btmb + ntpp
hdnj: rjmh * hzhd
vczv: 2
mgpc: 2
wjwv: tgsr * hcjt
rzdh: 2
lphz: 3
gqbp: 4
vzgr: zzpb * vhdw
vqzn: 6
cjrz: lnzc * bwnq
zcvl: qjwh / dclp
dvhd: 2
dhnm: nddz - tbsz
jnnm: jdmf - tzqz
bprb: humn - zntw
wrrs: wtcj + vbpf
mvrz: qmhv + prtj
lcgq: 2
zspd: 3
vjrr: 7
zdcs: pdrl * ggbr
qsqc: 9
hlcg: qzfw - dcfh
ntzh: jgwv + bblr
shjj: rfbf * whnh
pmwj: 5
scnb: 2
nsjf: ddsr + tvcf
pjbg: 2
pdrl: pmzv + qtpn
pldv: zbrj + rhbr
ljdn: 20
jzqv: 5
gtfs: 4
tjrp: dtfw * qzrn
rtgt: 4
mdfj: wfbn - brhw
bbqz: shjp * sfsn
nbjc: zqwh * vnps
zvjz: zdbw - wngz
bphq: 9
mmcv: 2
pblt: 9
cbwn: 2
hfdh: 2
qhcr: 14
fvmj: 5
cbwr: gdsj * rvvn
rvjn: dvjz * plwb
bhtw: smth * plpf
fmfd: vjsz + ftbf
jphn: 3
tnql: zcbc * lrrv
nwcs: rgbj * shwn
rhvs: dvdh + jwhr
pdsr: 5
lvnd: zlmq + ccwt
crbt: 2
dphw: 3
lrqr: 10
chcs: 2
ncvq: zsrr * cprn
mcns: 15
mphh: 5
ggbr: 3
shpp: 3
gnrt: bvmr * cjwv
sczm: gblm - hdsz
sbzt: 4
mdpb: lqbg / vgzw
tvqp: zhcg * cpcb
nnpm: vtbj + jhsl
lnzc: 5
mrqj: 4
csrw: rdjb * nqjr
lftj: ndts * shpm
gtrh: cjst + dwzb
zcdr: 2
hshg: 2
qncn: 7
hwpj: gwcm * gfbl
qzfw: 8
rvvl: lwnj * whgw
zwnr: wbnz * jbqz
fzcp: 2
vlpp: 2
cjst: mplm + zgwh
vflf: svfz + njjn
fwqm: lnhm + szrs
pdmr: mphh * msnj
blwz: tqfw * cqbn
qsmn: grgg * nldv
nhjc: 4
tzqz: 4
vcwv: mvrz * hmwp
prbd: 2
zcvp: 8
vbcs: 5
qppw: 3
hpdj: 3
tdvn: 13
szwb: jgrw + scrs
hcnj: mclp + zpvj
dtnc: 2
hghw: qwzp + scgq
ngtn: 3
ppjl: ggwb / vbtp
ltvp: 2
bpdh: tcqp * nssz
bwjc: 5
hnzn: 2
nhtp: 4
njbn: 13
jhfw: 18
tmzz: 2
qrzb: qqzm * vchf
mvhh: hdll + mwfz
jtfd: vnrt * fjvf
dmhj: fzsw / wjdt
hcvm: flpp - nbds
zfrj: crbt * nrdc
bhff: ttpl / hldc
rplr: 2
cppg: 2
pwzf: zzsh * qtff
whnl: trqr + dbvc
pdjt: hbll * rqrd
nsvr: ctvz - scsn
vffg: 3
fpll: 4
qbsh: mqgm + rvwm
dqzv: 2
bznm: 2
nqvg: 2
bqct: srnn * psht
npbm: 4
rwzr: rszw + zpdw
nlzs: tpvf * ttbb
sjsn: vtfw * lphz
fpnc: 2
pgwt: 3
wqrh: 3
gvjj: 10
mdwn: 4
smng: 6
zvlr: 2
wghm: srvl * hnvc
vncb: 3
rtlp: 3
qhpl: dqzv * dmst
pzgq: 3
jmbw: 2
gnzj: 3
pctp: fwjl * jmpr
lggd: bwlh - ldtt
wmjg: 2
mlhh: qlzg / rvwg
prrq: 2
lftw: fbvg * mdwt
grlr: pgjs * sczm
bhfv: dhnm / bcvw
vhdw: 3
qvfc: 3
scrs: gvcv + rlnb
trpr: 2
ltlj: 2
dvnw: 2
tccp: 3
gbsf: sghs + njdv
shnr: 4
djls: 5
spfm: 2
vbrd: 2
dnbh: 2
wfzq: 5
cdrv: hsdr / trpr
whsq: vdbj + vfld
hwmn: npgb / wmps
cvht: 2
vrhz: ncwg * gnnn
wgsr: 3
qhvw: hdzv / rzdh
cpff: zdcv - zqvf
qtbn: fdnn * pglg
ljsn: glqv * hsgh
zbrj: plnt * rrnh
ttbb: lnvp * qpcl
dclh: fcsq / wgrr
prcf: 5
mtwq: 3
cnrz: qcsh + vngl
gwdv: mdfj + chcs
fjbb: wjgv * wnls
hggc: mmgg * rnnh
tvhd: qscj * zwgv
jwlh: 2
qdqg: 13
dbpc: pblt + qtdr
ghsc: qhcr * vjrr
qfwd: ltps + sjjr
mwnm: 5
pjpg: hqqd * zzjd
msnj: 2
fgjf: ppjl - wttp
bcvw: 5
ftbg: 2
jwzp: 2
vnrt: 3
rgnq: 15
njvq: qqsv * mdrq
cbpw: rnlm + dcwb
lscv: bhrt + zwqt
jsnt: 10
dwzb: mjdf + dfhg
dgqj: fqff + vswr
hdzv: bcbn * gpvw
njdv: 16
zcbc: 12
cwtt: 4
cnpn: 2
vjqt: blvv + sjbc
vnjc: 5
plsp: 4
lwnj: 3
mdpl: 11
srrd: 15
zdmq: 3
vrnf: qqwm + nzpt
pgll: 7
fwrb: rbfq + zqdv
qhsf: 2
sqjc: lqzb * zzwn
pqpl: ljdn + tgng
tbwf: 2
rcnc: 11
qtcq: 12
gjfw: 2
hfgh: pnzb + frgv
qgrv: cfrh * jbbr
mzgc: 6
jsrh: vhrp * grhv
cbls: 2
jpwv: nhtp * mjth
vwqh: 10
ptfs: vdpd * jcjq
zvln: mqhh * czlt
mplm: prrq * bftw
brdf: nshb + ddjs
bgbr: 2
mmgg: bfvh * bdmv
mwfr: ncwn * clmg
rhbj: 4
lnhm: blbw + wmjj
gthg: 3
rsmn: 3
pvnt: ptwf + rwrs
grlq: 2
pmlp: 7
pwmd: mpvs + pzqs
mzjz: mlsb + drvj
sbql: wcwn - jhgj
lnpr: rpvn * sdbh
brlc: lrjl * gjhb
qrwj: 7
hwmm: jspd * wdrd
tcrm: hlgv + zrrv
fchd: lmbr * szml
vvff: 4
blbw: cftr * fftf
zfqn: zdcs * hvrb";

            solver.Initialize(input1);
            Assert.Multiple(() =>
            {
                Assert.That(solver.SolveFirstPart().Last(), Is.EqualTo("152"));
                Assert.That(solver.SolveSecondPart().Last(), Is.EqualTo("301"));
            });
            solver.Initialize(input2);
            Assert.Multiple(() =>
            {
                Assert.That(solver.SolveFirstPart().Last(), Is.EqualTo("78342931359552"));
                Assert.That(solver.SolveSecondPart().Last(), Is.EqualTo("3296135418820"));
            });
        }

    }
}