# 知识点
0. 2D游戏，设置Camera的Projection为Orthographic，Default Behavior Mode为2D模式
1. 图片精灵：Sprite Mode设置为Multiple模式，Sprite Editor中点击Slice切割图片
2. 屏幕中物体的层级通过Sorting Layer和Order In Layer调节
3. SpringJoint2D组件：连接两个物体，做弹簧似的往返运动。
    - 给小鸟设置SpringJoint2D组件，给右边弹弓设置RigidBody2D组件，设置type为static，不受重力影响。
    - SpringJoint2D组件设置：设置Distance（距离）、Frequency(弹簧的强度)
4. 给小鸟添加`Box Colider`组件，可以点击；让小鸟的位置随鼠标移动
    - 先让屏幕坐标转化为世界坐标`Camera.main.ScreenToWordPoint(Input.mousePosition)`
5. 给右边的弹弓right添加一个rightPos的空子物体，用来设置位置；在小鸟和rightPos之间设置一个最大的距离。
6. 小鸟飞出
    - isKinematic 在按下时设置为true，抬起时设置为false （为了减缓飞出去的速度，按下后不计算动力，鼠标抬起才计算）
    - 0.1s后设置`SpringJoint2D`组件的enable为false（为了小鸟飞出去有速度，也就是在鼠标抬起时，给0.1s的时间让小鸟形成一个动力飞出去）
7. 猪的受伤
    - 创建一个空物体，创建地面（加上Box Colider，与小鸟有碰撞，12份）
    - 给小鸟加个在地面上滚动的阻力，也就是空气阻力，将`Angular drag`设置为2
    - 在场景中添加小猪、添加RigidBody 2D 和Circle Colider 2D的组件，并将Angular drag设置为2
    - 给猪加脚本，在`OnCollisonEnter2D`里检测与小鸟的碰撞，当碰撞后的相对速度大于最大值，则销毁小猪；大于最小速度，则让小猪受伤（让小猪的图片sprite改成104）
8. 弹弓划线操作
    - 给左右弹弓加个`Line Renderer`的组件，并将其颜色改为皮带的颜色，大小改为皮带的大小0.25
    - Bird的update中每次位置改变，从两个弹弓到小鸟之间绘制两条直线作为皮带
9. 死亡、加分特效的制作
    - 找到那几个动画的图片拖到场景中，制作成动画boom，让动画的Loop Time设置为false；给boom加脚本，添加一个消失的方法；在Animation中的最后一帧，添加消失的事件；制作成prefab
    - 给猪死亡后加上爆炸的效果
    - 猪死后，在猪的头顶加上分值，将分值制作成prefab
10. 游戏逻辑的判定、实现多只小鸟的飞出
    - 加一个GameManager，管理游戏（3只小鸟，2只猪）
    - GameManager的初始化方法：启用第一只小鸟的脚本和SpringJoint2D，其它小鸟都禁用
    - 在Bird脚本中，当小鸟飞出去后，移除并销毁小鸟；在GameManager的Nextbird方法中判定游戏是否结束、成功、或拉去下一只小鸟
11. 解决划线问题、小鸟间转换速度问题
    - 在GameManager中记录第一个小鸟的位置，每次初始化时，都将那个位置赋值给集合中的第一个小鸟，这样小鸟就不会受弹力的作用，猛的升上去
    - 在鼠标抬起时，将划线的组件禁止，在调用划线方法Line开始出启用划线组件
12. 添加小鸟飞出去的拖尾效果
    - 引入拖尾的package，给小鸟添加一个子空物体trail
    - trail中加入weapon脚本组件，修改material材质以及其它属性
    - bird中加入一个BirdTrail的脚本
    - 小鸟飞出去时，开启拖尾；小鸟碰撞地面或小猪后，结束拖尾
13. 整合场景，解决无法显示划线弹弓的问题
    - 选一个木块的图片拖到场景中woodCube，添加RigidBody2D和Box Colider2D以及Pig脚本，制作成prefab
    - 同样的远离，设置长木块、石头，石头的mass/maxSpeed/minSpeed设置的大些
    - 将猪、木块、石头组合成一个场景（放置在Player层）
    - 添加蓝天背景和小菜（看不见，就改变Sorting Layer）
    - 弹弓皮带无法显示，则改变天空背景的`Order in Layer`，改为-1
14. 添加失败、胜利的游戏UI界面
    - 将canvas的`UI Scale Mode`改为`Scale With Screen Size`
    - canvas/image，添加lose背景（按住alt键，填满屏幕）
    - lose下面再创建一个image，按住alt键上下填满，按住alt键左右放大，改变颜色和透明度
    - 在canvas中再添加一个retry按钮、一个home按钮、一个猪笑脸
    - 给lose UI的背景加个变暗的动画
    - 同样的原理复制一份游戏胜利的UI界面win
    - 在胜利的UI界面中添加一个脚本，在动画结束时，添加一个事件方法，调用Show()，显示星星
    - 在GameManager中，游戏失败，显示失败UI界面；游戏胜利，显示成功UI界面
15. 修改火花粒子系统
    - 把三颗星星加入到win的UI界面中，并调整它们的位置，开始时不显示
    - 导入root的package，创建一个空对象roots，把root下的物体拖拽到roots下（不需要root的效果），把Scaling Mode调整为Local（这样修改scale才会有效果），然后再修改transform中scale的x,y,z都为0.5，Start size也都减半
16. 渲染层级关系
    - Camera：Depth先绘制depth低的物体
    - 同一个camera：Sorting layer在前面的先绘制
    - Order in layer：小的先绘制
    - Inspector面板上的Layer：用做摄像机是否将某些Layer上的物体显示出来，不影响层级渲染
17. 把粒子系统显示在UI之前
    - 问题：`Canvas UI`是位于所有物体之上的，要让粒子prefab显示在`Canvas UI`上需要做些处理
    - Canvas默认是位于UI层，把Canvas的`Render Mode`改为`Screen Space-Camera`，创建一个Camera，命名为UICamera，并指定给Camera，UICamera的`Clear Flags`改为`Depth only`，`Culling Mask`改为UI（所以UICamera只拍摄UI层的物体，并且与`Main Camera`拍摄的物体进行一个深度颜色的融合，底层也就能显示其它物体），将Projection设置为Orthographic（因为是2D的）
    - 将roots的Layer改为UI，并移入Canvas中，并将下面两个子物体的Renderer的Sorting Layer改为Player
    - 也就是说：UICamera的depth大于Main Camera的deth，所以UICamera拍摄的UI层上的物体（Canvas）位于其它物体上面，而roots的Sorting Layer大于Canvas的其它物体，所以粒子效果最后能显示在最上面
18. 让星星一颗颗显示在屏幕上
    - 在ShowStars方法中判断小鸟的数量，以此来显示星星的数量，剩余小鸟的数量+1就是星星的数量
    - 让星星一颗颗显示，则要在IEnumerator方法中通关`return new WaitForSeconds(0.2f)`来将要显示的星星设置Active为true
19. 添加暂停的动画
    - 在Bird添加一个变量，如果飞出去了就不能再点击了
    - 在场景中添加一个暂停的按钮，再添加一个暂停遮罩面板，面板中分别有：播放按钮、重新播放按钮、home按钮
    - 给暂停遮罩面包pausePanel添加入场和出场的动画
20. 添加鼠标注册事件
    - 在`file/Building Setting`中将三个场景都拖过去
    - 在GameManager脚本中添加Replay和Home方法，通过`SceneManager.LoadScene`方法实现
    - 在win和lose面板中的retry和home按钮上添加Button组件，然后把GameManager物体拖过去，分别监听Relay和Home方法事件
    - pausePanel的动画
        - 给pausePanel添加pause和resume动画后，会自动生成一个pausePanel的Animator状态机
        - 需要给pausePanel单独添加一个PausePanel的脚本，因为每次暂停跟播放都有动画
        - `pausePanel Animator`中设置三个状态：default默认状态、pause暂停状态、resume继续播放状态；设置一个isPause的bool变量，默认状态下pausePanel是显示的，但是bg的透明度为0，leftPanel位于坐标-90；当isPause为true时，就会执行pause动画（过渡到pause状态）；当isPause为false时，就会执行resume动画（过渡到resume状态），resume到default之间的`Has Exit Time`必须勾选上，这样resume的动画执行完时，就会进入default默认状态
        - 在PausePanel脚本中有四个方法：
            - 点击场景中的pause按钮，执行Pause方法，Animator的isPause变量设置为false,按钮的Active设置为false
            - 当pause动画执行结束后，添加一个事件PauseAnimEnd，`Time.timeScale=0`，这样场景的物体就停止运行了
            - 当点击pausePanel面板中的resume按钮，执行Resume方法，先将`Time.timeScale=1`（这样动画才会执行），再讲Animator的isPause设置为false
            - 当resume动画执行结束后，添加一个事件ResumeAnimEnd，让按钮的Active变为true
21. 添加镜头跟随
    - 让摄像机的x轴的位置等于小鸟在飞行过程中的位置，并且给个限定，最小（初始0）到最大15
        ```c#
            float posX = transform.position.x;
            Vector3 initPos = Camera.main.transform.position;
            Camera.main.transform.position = Vector3.Lerp(initPos, new Vector3(Mathf.Clamp(posX, 0, 15), initPos.y, initPos.z), cameraFollowSpeeed * Time.deltaTime);
        ```js
    - 当一只小鸟飞出去后，下一只小鸟的脚本就会启动，所以下一只小鸟就位的时候，能再回到原点
22. 添加音效
    - 给小鸟添加：选择、飞出去、死亡的声音
    - 给猪和木头添加：撞击、死亡的声音
    - 添加方法：`AudioSource.PlayClipAtPoint(clip, transform.position)`，不在物体上绑定AudioSource组件，这样物体即使销毁，也可以正常播放死亡的声音
23. 添加黄色小鸟（飞行过程中，点击左键，获得一次飞行速度翻倍的机会）
    - 复制一份redBird，命名为yellowBird，把小鸟图片sprite替换为黄色小鸟，调整下RigidBody2D的大小
    - 在Bird脚本中，设置一个isFly的变量，在小鸟飞出去后isFly为true，在update方法中，当isFly为true并且点击了鼠标左键，就给小鸟添加特效，执行ShowSkills的方法
    - YellowBird脚本继承Bird，重新实现ShowSkills，让RigidBody2D的velocity*2
    - 制作成prefab
24. 添加绿色小鸟（飞行过程中，点击左键，获得一次飞行方向反转的机会，也就X轴上的速度翻转）
    - 添加方法同上
    - 给所有小鸟添加受伤的图片，在Bird脚本中增加Hurt方法（替换为受伤的图片），在Pig碰撞检测事件中调用改方法（不在Bird的碰撞检测中调用，因为游戏开始时，小鸟会与地面碰撞）
25. 添加黑色小鸟（点击小鸟，周围的物体全都消失）
    - 制作blackBird，再添加一个`Circle Collider`，把radius设为1，isTrigger设为true，
    - 添加BlackBird脚本，监听OnTriggerEnter2D事件，把触碰到的物体加入到blocks集合中；监听OnTriggerExit2D事件，blocks集合中移除对象
    - 因此ShowSkill方法中，可以遍历blocks集合，让集合中的每个对象都Dead
26. 处理黑色小鸟爆炸问题
    - 增加一个OnClear方法，在ShowSkill中调用，OnClear方法中完成速度清零、添加爆炸效果、renderer不可见、禁用CircleCollider2D、停止拖尾
    - 重写Next方法，去掉爆炸效果
27. 处理星星数组越界的问题
    - 在GameManager的show方法中，当大于星星数时，直接break
28. 制作选择地图UI界面（01-level）
    - 创建背景bg，填充背景，导入的图片先`Set Native Size`，再调整大小，按住alt键，以中心点进行缩放
    - 再关卡面板，如果可以玩，添加星星star（已获取的星星/总星星）；否在显示锁+需要的星星数lock
    - 第一关默认是可以直接玩的，后面两关需要解锁，第四个是个纯图片
29. 地图选择的逻辑编写
    - 给mapPanel添加脚本MapSelect，脚本设置starsNum(每一关解锁需要的星星数)
    - 通过PlayerPrefs(存储数据用的)获取当前所有的星星总数totalNum，如果大于starsNum，就让lock显示，star隐藏
30. 制作关卡选择界面
    - 创建背景，添加一个返回按钮，添加一个grid物体（含有`Grid Layout Group`组件，用来给关卡布局）
    - 创建一个levelSelect，添加背景、解锁背景、关卡数、星星、脚本LevelSelect，放入grid中，让第一个关卡可点击，其它都是成未解锁状态
31. 添加地图选择和关卡选择界面的交互
    - 再MapSelect脚本中添加Selected方法，点击mapPanel时，跳转到对应的关卡选择界面panel
32. 存储关卡星星数据
    - 需要存储的数据
        - 每一关完成的星星数量，涉及更新，涉及下一关是否可以开启，以及level界面星星个数的显示
        - 星星数量的总和，便利所有星星数非0的关卡星星数，然后相加
        - 当前选择的关卡名字，用于关卡的加载
    - 步骤
        - 在LevelSelect脚本中，存储当前选择的关卡，key为nowLevel， value为level+关卡的名字（数字）
        - 在GameManager脚本中，游戏结束（Replay和Home方法中）先获取nowLevel，再存储当前关卡获取的星星数starsNum
33. 选择关卡，场景跳转
    - 选择02-game场景，创建一个空对象level1（0,0,0)，把所有物体都移入level1，放入Resources(动态加载资源，看下换装和BoundleAssets的视频教程)
    - 制作level2，把block和pig重新搭下
    - 点击按钮，加载game场景，在Main Camera上绑定LoadScene脚本，Awake方法中加载level资源
        ```c#
            print(PlayerPrefs.GetString("nowLevel"));
            Instantiate(Resources.Load(PlayerPrefs.GetString("nowLevel")));
        ```
    - 关卡的按钮的子级num和那三颗星星不能添加Button组件，不然关卡的按钮就不起作用
35. 处理关卡界面星星个数的 显示
    - 在LevelSelect脚本中，Start方法中，获取当前关卡的星星数，然后让对应的星星激活显示即可
    - 在SaveData保存当前关卡星星数时，更新totalNum的值
36. 关卡之间的切换
    - 在LevelSelect脚本中，Start方法，第一关以及上一关的获得的星星大于0，当前关isSelect都是true
37. 地图切换、制作多个关卡
    - 修改每个mapPanel下面的level数字，让每个level数字连续
38. snap setting使用介绍
    - 制作level3,更改block和猪， 更改level2的小鸟
    - 以同样的方法制作level4/5/6，只是更改了背景、草地、地板（可以更改小鸟、block）
    - 以同样的方法制作level7/7/9，只是更改了背景、草地、地板（可以更改小鸟、block）
39. 地板信息的显示
    - 给每个panel上的返回按钮注册事件
    - 更改mapPanel上star Text的数值
    - 给01-level场景加个音乐（在Camera上面加）
40. 异步加载场景
    - 在loading场景中，设置背景，正在加载的文字，添加一个脚本，2s后就LoadScene(1)
41. 游戏发布
    - 在游戏开始加载页面，设置游戏屏幕的大小`Screen.SetResolution(960, 600, false)`,免得变形
    - File/Play Setting，设置游戏名字等，点击发布
42. 修改bug
    - 点击第2只、3只小鸟等，虽然不会移动，但是鼠标抬起后，小鸟会爆炸消失：虽然禁止了脚本，然后会响应mouse事件，所有要把脚本中的canClick设置为public，在GameManager寻找下一只小鸟的时候，再把第一只设置为true,其余小鸟都为false
    - 暂停游戏之后，再点击第一只小鸟，然后会画线：在Bird中再设置一个变量isReleased，当小鸟飞出去的时候，就为true，那么暂停游戏那一刻，应该先判断第一只小鸟是否还没飞出去，如果没飞出去，就让canClick为false，在Resume方法总，让第一只小鸟的canClick为true
    - 特技小鸟飞出去后，如果点击了暂停，仍然会执行特技方法：在Bird的Update方法先判断是否鼠标与EventSystem(canvas)发生了交互，如果是就直接返回(`EventSystem.current.IsPointerOverGameObject()`)